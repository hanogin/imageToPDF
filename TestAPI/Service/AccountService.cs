using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Serilog;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class AccountService
    {
        private readonly SportiveContext _context;
        private readonly IDapperBaseRepository _con;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(
        IHttpContextAccessor httpContextAccessor,
        SportiveContext context,
        IDapperBaseRepository dapper,
        ILogger logger)
        {
            _context = context;
            _con = dapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<AccountRes> GetByFilter(GetWithFilter model)
        {
            //            var query = @"
            //SELECT acc.AccountId,
            //       acc.actionDate,
            //       acc.ClientId,
            //       client.FirstName || ' ' || client.LastName AS ClientFullName,
            //       acc.subscriptionId,
            //       (
            //           SELECT CASE WHEN acc.amount < 0 THEN (
            //                          SELECT prog.name
            //                            FROM App_Program AS prog,
            //                                 App_Subscription AS sub
            //                           WHERE prog.ProgramId = sub.programId AND 
            //                                 sub.ProgramId = acc.subscriptionId
            //                      )
            //                  WHEN acc.amount > 0 THEN NULL END [END]
            //       )
            //       AS programName,
            //       (
            //           SELECT pt.PaymentTypeDesc
            //             FROM T_PaymentType pt
            //            WHERE pt.PaymentTypeId = acc.paymentTypeId
            //       )
            //       AS PaymentMethodDesc,
            //       acc.amount,
            //        CASE  acc.amount 
            //           WHEN acc.amount > 0 
            //               THEN 'הפקדה' 
            //           ELSE 'רכישה' 
            //       END ActionTypeDesc
            //  FROM App_Account acc
            //       JOIN App_Client client ON client.ClientId = acc.ClientId
            //  WHERE (@ClientId IS NULL OR acc.ClientId = @ClientId) 
            //        AND (@StartDate IS NULL OR acc.ActionDate > @StartDate)
            //        AND (@EndDate IS NULL OR acc.ActionDate < @EndDate)";
            //            var con = _con.GetConnection(null);
            //            con.Open();
            //            return con.Query<AccountRes>(query, model).ToList();
            //return _con.Query<AccountRes>(query, model).ToList();

            var query = _context.AppAccounts.AsQueryable();

            if (model.ClientId.HasValue)
            {
                query = query.Where(x => x.ClientId == model.ClientId.Value);
            }

            if(!model.StartDate.HasValue && !model.EndDate.HasValue && !model.ClientId.HasValue)
            {
                query = query.Where(x => x.ActionDate >= DateTime.Now.AddDays(-7));

                query = query.Where(x => x.ActionDate <=  DateTime.Now);
            }

            if (model.StartDate.HasValue)
            {
                query = query.Where(x => x.ActionDate.Date >= model.StartDate.Value.Date);
            }

            if (model.EndDate.HasValue)
            {
                query = query.Where(x => x.ActionDate.Date <= model.EndDate.Value.Date);
            }

            return query.Select(x => new AccountRes
            {
                AccountId = x.AccountId,
                ClientId = x.ClientId,
                ActionDate = x.ActionDate,
                ClientFullName = x.Client.FirstName + " " + x.Client.LastName,
                ProgramName = x.Subscription.Program.Name,
                ActionTypeDesc = x.Amount <= 0 ? "רכישה" : "הפקדה",
                Amount = x.Amount,
                ActionByUserName = x.User.FirstName + " " +x.User.LastName,
            }).ToList();
        }

        public void AddDeposit(AccountAddDeposit value)
        {
            var user = (UserRes)_httpContextAccessor.HttpContext.Items["User"];
            _context.AppAccounts.Add(new AppAccount
            {
                ClientId = value.ClientId,
                Amount = value.Amount,
                ActionDate = value.Date,
                UserId = user.UserId
            });

            _context.SaveChanges();
        }
        public void Delete(int accountId)
        {
            //var query = "DELETE FROM App_Account WHERE AccountId = @AccountId";
            //_con.Execute(query, new { AccountId = accountId });

            var entity = _context.AppAccounts.Where(x => x.AccountId == accountId).First();
            _context.AppAccounts.Remove(entity);
            _context.SaveChanges();
        }
    }
}
