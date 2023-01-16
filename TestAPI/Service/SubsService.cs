using DAL.Context;
using DAL.Entities;
using Serilog;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class SubsService
    {
        private readonly SportiveContext _context;
        private readonly IDapperBaseRepository _con;
        private readonly ILogger _logger;

        public SubsService(
         SportiveContext context,
        IDapperBaseRepository dapper,
        ILogger logger)
        {
            _con = dapper;
            _logger = logger;
            _context = context;

        }

        public List<SubscriptionRes> GetAll(GetWithFilter model)
        {
            var query = _context.AppSubscriptions.AsQueryable();

            if (model.ClientId.HasValue)
            {
                query = query.Where(x => x.ClientId == model.ClientId.Value);
            }

            if (!model.StartDate.HasValue && !model.EndDate.HasValue && !model.ClientId.HasValue)
            {
                query = query.Where(x => x.ActionDate >= DateTime.Now.AddDays(-7));

                query = query.Where(x => x.ActionDate <= DateTime.Now);
            }

            if (model.StartDate.HasValue)
            {
                query = query.Where(x => x.ActionDate.Date >= model.StartDate.Value.Date);
            }

            if (model.EndDate.HasValue)
            {
                query = query.Where(x => x.ActionDate.Date <= model.EndDate.Value.Date);
            }


            return query.Select(x => new SubscriptionRes
            {
                SubscriptionId = x.SubscriptionId,
                ClientFullName = x.Client.FirstName + " " + x.Client.LastName,
                ProgramName = x.Program.Name,
                ActionDate = x.ActionDate,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Visit = x.Visit,
                Visited = x.AppVisits.Count(),
                VisitsLeft = x.Visit - x.AppVisits.Count(),
                Price = x.Price,
                ProgramId = x.ProgramId,
                Comment = x.Comment,
                AlertType = (DateTime.Now > x.EndDate || (x.Visit - x.AppVisits.Count()) <= 0) ? "danger" : ((x.EndDate - DateTime.Now).TotalDays / 7) <= 2 ? "warning" : ""
            }).ToList();
            //            return _con.Query<SubscriptionRes>(@"
            //SELECT SubscriptionId,
            //       client.FirstName || ' ' || client.LastName AS ClientFullName,
            //       prog.name AS ProgramName,
            //       actionDate,
            //       startDate,
            //       endDate,
            //       Visit,
            //       (
            //           SELECT COUNT( * ) 
            //             FROM App_Visit vis
            //            WHERE vis.subscriptionId = sub.subscriptionId
            //       )
            //       Visited,
            //       (
            //           SELECT sub.visit - count( * ) 
            //             FROM App_Visit
            //            WHERE sub.subscriptionId = subscriptionId
            //       )
            //       VisitsLeft
            //  FROM App_Subscription sub
            //       JOIN
            //       App_Client client ON client.ClientId = sub.ClientId
            //       JOIN
            //       App_Program prog ON sub.programId = prog.programId
            //  WHERE (@ClientId IS NULL OR sub.ClientId = @ClientId) 
            //        AND (@StartDate IS NULL OR sub.StartDate > @StartDate)
            //        AND (@EndDate IS NULL OR sub.EndDate < @EndDate)");
        }

        public SubscriptionRes GetBySubId(int subId)
        {
            return _context.AppSubscriptions
                .Where(x => x.SubscriptionId == subId)
                .Select(x => new SubscriptionRes
                {
                    SubscriptionId = x.SubscriptionId,
                    ClientFullName = x.Client.FirstName + " " + x.Client.LastName,
                    ProgramName = x.Program.Name,
                    ActionDate = x.ActionDate,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Visit = x.Visit,
                    Visited = x.AppVisits.Count(),
                    VisitsLeft = x.Visit - x.AppVisits.Count(),
                    Price = x.Price,
                    ProgramId = x.ProgramId,
                    Comment = x.Comment,
                    AlertType = (DateTime.Now > x.EndDate || (x.Visit - x.AppVisits.Count()) <= 0) ? "danger" : ((x.EndDate - DateTime.Now).TotalDays / 7) <= 2 ? "warning" : ""
                }).First();
        }


        public List<SubscriptionActiveByClientRes> ActiveSubsByClientId(int clientId)
        {
            var cutoff = DateTime.Now.AddDays(-6);

            return _context.AppSubscriptions
                .Where(x => x.ClientId == clientId && x.StartDate < DateTime.Now && x.EndDate >= cutoff && x.Visit > x.AppVisits.Count())
                .Select(s => new SubscriptionActiveByClientRes
                {
                    SubscriptionId = s.SubscriptionId,
                    ProgramName = s.Program.Name
                }).ToList();

            //            var query = @"
            //SELECT sub.SubscriptionId,
            //       prog.Name AS ProgramName
            //  FROM App_Subscription sub
            //       JOIN
            //       App_Program prog ON prog.ProgramId = sub.ProgramId
            // WHERE startDate < DATE() AND 
            //       EndDate > DATE() AND 
            //       visit > (
            //                   SELECT COUNT( * ) 
            //                     FROM App_Visit vis
            //                    WHERE vis.subscriptionId = sub.SubscriptionId
            //               ); AND sub.ClientId = @ClientId";

            //            return _con.Query<SubscriptionActiveByClientRes>(query, new { ClientId = clientId }).ToList();
        }


        public void Create(SubscriptionCreate model)
        {
            //           var query = @"
            //INSERT INTO subscription
            //               (ClientId
            //               , ProgramId
            //               , StartDate
            //               , EndDate
            //               , Visit
            //               , Price
            //               , Comment)
            //          VALUES
            //               (@ActionType
            //               , @ClientId
            //               , @ProgramId
            //               , @StartDate
            //               , @EndDate
            //               , @Visit
            //               , @Price
            //               , @Comment)";
            //           _con.Execute(query, model);
            var entity = new AppSubscription
            {
                ClientId = model.ClientId,
                ProgramId = model.ProgramId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Visit = model.Visit,
                Price = model.Price,
                Comment = model.Comment,
                ActionDate = DateTime.Now
            };

            _context.AppSubscriptions.Add(entity);

            _context.SaveChanges();

            // Add account
            _context.AppAccounts.Add(new AppAccount
            {
                ActionDate = DateTime.Now,
                ClientId = model.ClientId,
                SubscriptionId = entity.SubscriptionId,
                Amount = model.Price * -1
            });

            _context.SaveChanges();
        }

        public void Update(SubscriptionUpdate model)
        {
            // Update subs
            var entity = _context.AppSubscriptions.Where(x => x.SubscriptionId == model.SubscriptionId).First();

            entity.ProgramId = model.ProgramId;
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;
            entity.Visit = model.Visit;
            entity.Price = model.Price;
            entity.Comment = model.Comment;

            // Update account price
            var entityAcount = _context.AppAccounts.Where(x => x.SubscriptionId == model.SubscriptionId).First();
            entityAcount.Amount = model.Price > 0 ? model.Price * -1 : model.Price;

            _context.SaveChanges();

            //    _con.Execute(@"
            //UPDATE App_Subscription 
            //    SET ClientId = @ClientId, 
            //        ProgramId = @ProgramId, 
            //        StartDate = @startDate, 
            //        EndDate = @EndDate, 
            //        Visit = @Visit, 
            //        Price = @Price
            //        Comment = @Comment
            //    WHERE SubscriptionId = @SubscriptionId", model);
        }

        public void DeleteSubs(int subsId)
        {
            // Get client id
            int clientId = _context.AppSubscriptions.Where(x => x.SubscriptionId == subsId).First().ClientId;

            // Delete visit
            var visitEntities = _context.AppVisits.Where(x => x.SubscriptionId == subsId).ToList();
            // Delete account row
            var accountEntities = _context.AppAccounts.Where(x => x.SubscriptionId == subsId).ToList();
            // Delete subs
            var subsEntities = _context.AppSubscriptions.Where(x => x.SubscriptionId == subsId).ToList();

            // Delete clientIn lesson
            var clientInLessonEntites = _context.AppClientInLessons.Where(x => x.ClientId == clientId).ToList();

            _context.AppClientInLessons.RemoveRange(clientInLessonEntites);
            _context.AppAccounts.RemoveRange(accountEntities);
            _context.AppVisits.RemoveRange(visitEntities);
            _context.AppSubscriptions.RemoveRange(subsEntities);

            _context.SaveChanges();
        }
    }
}
