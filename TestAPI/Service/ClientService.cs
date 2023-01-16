using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Service.DTO;
using Service.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.IO;

namespace Service
{
    public class ClientService
    {
        private readonly SportiveContext _context;
        private readonly IDapperBaseRepository _con;
        private readonly ILogger _logger;

        public ClientService(
        SportiveContext context,
        IDapperBaseRepository dapper,
        ILogger logger)
        {
            _context = context;
            _con = dapper;
            _logger = logger;
        }

        public List<ClientForSearchRes> GetAll()
        {
            var reView = _context.VClientWithSubs.ToList().DistinctBy(x => x.ClientId).ToList();

            var cutoff = DateTime.Now.AddDays(-6);

            var resEf = _context.AppClients.Select(x => new ClientForSearchRes
            {
                ClientId = x.ClientId,
                FullName = x.LastName + " " + x.FirstName,
                Phone1 = x.Phone1,
                Phone2 = x.Phone2,
                FullNameWithPhone1 = x.LastName + " " + x.FirstName + " - " + x.Phone1,
                Email = x.Email,
                IsActiveSubs = x.AppSubscriptions.Any
                       (subs => subs.ClientId == x.ClientId && ((subs.StartDate <= DateTime.Now && subs.EndDate >= DateTime.Now)
                                                                   || (subs.EndDate >= cutoff))),
                VisitsLeft =
               x.AppSubscriptions.Any(subs => subs.ClientId == x.ClientId &&
                                      ((subs.StartDate <= DateTime.Now && subs.EndDate >= DateTime.Now) || (subs.EndDate >= cutoff))) ?
                                       x.AppSubscriptions
                                       .Where(subs => subs.ClientId == x.ClientId &&
                                       ((subs.StartDate <= DateTime.Now && subs.EndDate >= DateTime.Now) || (subs.EndDate >= cutoff)))
                                       .FirstOrDefault().Visit - x.AppVisits.Count() : null,
                Balance = x.AppAccounts.Sum(v => v.Amount),
                KupatHolimId = x.KupatHolimId,
                SubsId = x.AppSubscriptions.Where(subs1 => subs1.ClientId == x.ClientId &&
                                       ((subs1.StartDate <= DateTime.Now && subs1.EndDate >= DateTime.Now) || (subs1.EndDate >= cutoff))).FirstOrDefault().SubscriptionId
            }).ToList();

            return resEf;

        }

        /// <summary>
        /// For manuall add client to lesson - that not exist in current lesson
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public List<ClientForAddManuall> GetClientForManuall(int lessonId)
        {
            var cutoff = DateTime.Now.AddDays(-6);

            // (Active || Future || Finish in range) && Has more visit && Not in lesson || 
            var clientWithSubs = _context.AppSubscriptions
                .Where(subs => ((subs.StartDate <= DateTime.Now && subs.EndDate >= DateTime.Now) ||
                                    (subs.EndDate >= cutoff) || subs.StartDate > DateTime.Now)
                                    && subs.Visit > subs.AppVisits.Count()
                                    && !_context.AppClientInLessons.Any(x => x.ClientId == subs.ClientId && x.LessonId == lessonId))
                .Select(x => new ClientForAddManuall
                {
                    ClientId = x.ClientId,
                    SubscriptionId = x.SubscriptionId,
                    ClientFullName = x.Client.LastName + " " + x.Client.FirstName + " - " + x.Client.Phone1,
                    VisitLeft = x.Visit - x.AppVisits.Count(),
                    SubStatus = x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now ? (int)SubStatus.Active :
                                   (x.StartDate <= DateTime.Now && x.EndDate >= cutoff) ? (int)SubStatus.AboutToFinish : (int)SubStatus.Future,
                    SubsEndDate = x.EndDate,
                    //ClientFullName = x.Client.LastName + " " + x.Client.FirstName,
                    ProgramName = x.Program.Name,
                    SubsStartDate = x.StartDate,
                    Balance = x.AppAccounts.Sum(x => x.Amount)
                }).ToList();

            // Get all client
            var client = _context.AppClients
                .Where(s => !s.AppClientInLessons.Any(x => x.LessonId == lessonId))
                .Select(x => new ClientForAddManuall
                {
                    ClientId = x.ClientId,
                    ClientFullName = x.LastName + " " + x.FirstName + " - " + x.Phone1
                }).ToList();

            client = client.Where(x => !clientWithSubs.Any(clientSubs => clientSubs.ClientId == x.ClientId)).ToList();

            clientWithSubs.AddRange(client);

            foreach (var item in clientWithSubs)
            {
                item.Result = item.ClientFullName;

                if (item.SubStatus == 1)
                {
                    item.Result += " | " + " סטטוס המנוי: " + "פעיל";
                }

                else if (item.SubStatus == 2)
                {
                    item.Result += " | " + " סטטוס המנוי: " + "עתידי";
                }

                else if (item.SubStatus == 0)
                {
                    item.Result += " | " + " סטטוס המנוי: " + "לקראת סיום";
                }
                else
                {
                    item.ProgramName = "ביקור חד פעמי";
                    item.SubsStartDate = null;
                    item.SubsEndDate = null;
                }
            }

            return clientWithSubs.OrderBy(x => x.ClientFullName).ToList();
        }


        public List<ClientRes> GetById(int clientId)
        {
            var query = @"
		                SELECT ClientId
			                  ,FirstName
			                  ,LastName
                              ,FirstName + ' ' + LastName FullName
			                  ,IdentityId
			                  ,BirthDate
			                  ,KupatHolimId
                              ,ClientTypeId
			                  ,Phone1
			                  ,Phone2
			                  ,Email
			                  ,Address
		                  FROM App_Client
		                  WHERE ClientId = @ClientId;";

            return _con.Query<ClientRes>(query, new { ClientId = clientId });
        }

        public int Create(ClientCreate model)
        {
            // return _con.ExecuteScalar<int>(@"
            // INSERT INTO App_Client 
            //     (IdentityId
            //     ,BirthDate
            //     ,FirstName
            //     ,LastName
            //     ,Phone
            //     ,Cell
            //     ,KupatHolimId
            //     ,Address
            //     ,Email)
            //VALUES 
            //     (@IdentityId
            //     ,@BirthDate
            //     ,@FirstName
            //     ,@LastName
            //     ,@Phone
            //     ,@Cell
            //     ,@KupatHolimId
            //     ,@Address
            //     ,@Email); SELECT last_insert_rowid()", model);

            var entity = new AppClient
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                IdentityId = model.IdentityId,
                BirthDate = model.BirthDate,
                KupatHolimId = model.KupatHolimId,
                ClientTypeId = model.ClientTypeId,
                Phone1 = model.Phone1,
                Phone2 = model.Phone2,
                Email = model.Email,
                Address = model.Address
            };

            _context.AppClients.Add(entity);

            _context.SaveChanges();

            return entity.ClientId;
        }

        public void Update(ClientUpdate model)
        {
            var entity = _context.AppClients.Where(x => x.ClientId == model.ClientId).First();

            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.IdentityId = model.IdentityId;
            entity.BirthDate = model.BirthDate;
            entity.KupatHolimId = model.KupatHolimId;
            entity.ClientTypeId = model.ClientTypeId;
            entity.Phone1 = model.Phone1;
            entity.Phone2 = model.Phone2;
            entity.Email = model.Email;
            entity.Address = model.Address;

            _context.SaveChanges();

            //    _con.Execute(@"
            //Update App_Client 
            //    SET
            //        IdentityId = @IdentityId,
            //        BirthDate = @BirthDate,
            //        FirstName = @FirstName, 
            //        LastName = @LastName,
            //        Phone = @Phone, 
            //        Cell = @Cell, 
            //        KupatHolimId = @KupatHolimId,
            //        Email = @Email,
            //        Address = @Address
            //    WHERE ClientId = @ClientId", model);
        }

        #region File

        public List<ClientFileNamesRes> GetFileNamesByCLientId(int clientId)
        {
            return _context.AppClientFiles.Where(x => clientId == x.ClientId)
                .Select(x => new ClientFileNamesRes
                {
                    ClientFileId = x.ClientFileId,
                    ClientId = x.ClientId,
                    FileName = x.FileName,
                    FileTypeId = x.ClientFileTypeId,
                    FileTypeDesc = x.ClientFileType.ClientFileTypeDesc,
                    Comment = x.Comment,
                    ActionDate = x.ActionDate.ToString("dd-MM-yyyy")
                }).ToList();
        }

        public void CreatFile(ClientFileCreate file)
        {
            _context.AppClientFiles.Add(new AppClientFile
            {
                ClientId = file.ClientId,
                File = Convert.FromBase64String(file.FileAsBase),
                ClientFileTypeId = file.FileTypeId,
                Comment = file.Comment,
                FileName = file.FileName,
                FileType = file.FileType
            });

            _context.SaveChanges();
        }

        public AppClientFile GetFile(int clientFileId)
        {
            return _context.AppClientFiles.Where(x => x.ClientFileId == clientFileId).First();
        }

        public void DeleteFile(int clientFileId)
        {

            _con.Execute($"DELETE FROM App_ClientFile WHERE ClientFileId = @clientFileId", new { clientFileId = clientFileId });
        }
        #endregion
    }
}
