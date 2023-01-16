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
    public class ReportService
    {
        private readonly SportiveContext _context;
        private readonly IDapperBaseRepository _con;
        private readonly ILogger _logger;

        public ReportService(
        SportiveContext context,
        IDapperBaseRepository dapper,
        ILogger logger)
        {
            _context = context;
            _con = dapper;
            _logger = logger;
        }

        public List<ReportMacabyRes> GetByDate(DateTime date)
        {
            date = date.AddMonths(-1);
            //DateTime lastDay = date.AddDays(-1);
            //DateTime starOfMonth = new DateTime(lastDay.Year, lastDay.Month, 1);
            //int selectedMonth = date.Month;
            var query = @"
select 
     cli.ClientId
	,cli.FirstName
	,cli.LastName
	,cli.Phone1
	,cli.identityId
	,cli.address
	,sub.StartDate
	,sub.EndDate
	,sub.SubscriptionId
	,prog.Name AS ProgramName
	    ,(SELECT 
                COUNT(history1.MaccabyReportHistoryId) 
                FROM [dbo].[App_Maccaby_ReportHistory] history1 
                WHERE history1.SubscriptionId = sub.SubscriptionId
	    ) AS ReportCount
    ,prog.NumberOfmonth
from App_Client cli
JOIN T_KupatHolim kupat on kupat.KupatHolimId = cli.KupatHolimId 
JOIN App_Subscription sub ON sub.clientId = cli.ClientId
JOIN App_Program prog on  prog.ProgramId = sub.ProgramId
where kupat.KupatHolimId = 6
AND @date between DATEADD(mm, DATEDIFF(m,0,sub.startDate),0) AND cast(DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,sub.endDate)+1,0)) as date)
AND NOT EXISTS 
	(
		SELECT * 
		FROM [dbo].[App_Maccaby_ReportHistory]  mcHistory
		WHERE mcHistory.subscriptionId = sub.subscriptionId AND mcHistory.ReportMonth = @date
	)
";
            return _con.Query<ReportMacabyRes>(query, new { date = date }).ToList();
        }
    }
}
