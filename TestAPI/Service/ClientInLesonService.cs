using DAL.Context;
using DAL.Entities;
using Serilog;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class ClientInLesonService
    {
        private readonly SportiveContext _context;
        private readonly IDapperBaseRepository _con;
        private readonly ILogger _logger;

        public ClientInLesonService(
        SportiveContext context,
        IDapperBaseRepository dapper,
        ILogger logger)
        {
            _context = context;
            _con = dapper;
            _logger = logger;
        }

        public List<ClientInLessonRes> GetClientInLesson(int lessonId, DateTime date)
        {
            var query = @"
SELECT * 
FROM
	(SELECT
		client.ClientId,
		client.LastName + ' ' + client.FirstName + ' - ' + client.Phone1 AS ClientFullName,
		sub.startDate AS SubsStartDate,
		sub.endDate AS SubsEndDate,
		cil.JoinDate,
		sub.SubscriptionId,
		sub.Visit - (SELECT COUNT(*) FROM [dbo].[App_Visit] vis WHERE vis.SubscriptionId = sub.SubscriptionId) AS VisitLeft,
		prog.Name ProgramName,
		(SELECT SUM(acc.amount) FROM [dbo].[App_Account] acc WHERE acc.clientId = cil.ClientId) AS Balance,
		ROW_NUMBER() OVER(PARTITION BY client.ClientId ORDER BY sub.SubscriptionId DESC) AS row,
		(
			SELECT CASE 
					 WHEN EXISTS (
								SELECT 1
								FROM [dbo].[App_Visit] vis1
								WHERE vis1.SubscriptionId = sub.SubscriptionId AND CONVERT(VARCHAR(10), @date, 102) = vis1.Date AND vis1.LessonId =  @LessonId
								) 
						 THEN 1
						 ELSE 0
			        END
		) IsVisitToday,
subStatusView.SubsStatusId,
subStatusView.SubsStatusStyle,
subStatusView.SubsStatusLabel
	FROM [dbo].[App_ClientInLesson] cil
		LEFT JOIN [dbo].[App_Client] client ON client.clientId = cil.ClientId
		LEFT JOIN [dbo].[App_Subscription] sub ON sub.clientId = cil.ClientId
		LEFT JOIN [dbo].[App_Program] prog ON prog.programId = sub.programId
        LEFT JOIN [dbo].[V_SubscriptionsStatus] subStatusView on subStatusView.SubscriptionId = sub.SubscriptionId
	WHERE 
		 cil.LessonId = @LessonId
	) AS a
WHERE row = 1
ORDER BY ClientFullName";


            return _con.Query<ClientInLessonRes>(query, new {date = date, LessonId = lessonId }).DistinctBy(x => x.ClientId).ToList();
            //  return _context.AppClientInLessons.Where(x => x.LessonId == lessonId)
            //.Select(s => new ClientInLessonRes
            //{
            //    LessonId = s.LessonId,
            //    ClientId = s.ClientId,
            //    JoinDate = s.JoinDate,
            //    ClientFullName = s.Client.FirstName + " " + s.Client.LastName,
            //    SubscriptionId = s.Client.AppSubscriptions.Any(sub => IsSubscriptionValid(sub)) ?
            //    s.Client.AppSubscriptions.Where(sub => IsSubscriptionValid(sub)).First().SubscriptionId : null,
            //    ProgramName = s.Client.AppSubscriptions.Any(sub => IsSubscriptionValid(sub)) ?
            //    s.Client.AppSubscriptions.Where(sub => IsSubscriptionValid(sub)).First().Program.Name : null,
            //    VisitLeft = s.Client.AppSubscriptions.Any(sub => IsSubscriptionValid(sub)) ?
            //    s.Client.AppSubscriptions.Where(sub => IsSubscriptionValid(sub)).First().Visit - s.Client.AppVisits.Where() : null,


            //}).ToList();
        }

        // Get all active client that not exist in given lesson
        public List<ClientInLessonRes> GetClientNotInLesson(int lessonId)
        {
            var cutoff = DateTime.Now.AddDays(-6);

            return _context.AppClients
                .Where(x =>
                // (Active || Future || End but 6 days not pass) && still has visit
                       x.AppSubscriptions.Any(sub => (sub.StartDate <= DateTime.Now && sub.EndDate >= DateTime.Now 
                       || sub.StartDate > DateTime.Now || (sub.EndDate >= cutoff)) && (sub.Visit > sub.AppVisits.Count()))
                       && !x.AppClientInLessons.Any(inLes => inLes.LessonId == lessonId && inLes.ClientId == x.ClientId))
                .Select(s => new ClientInLessonRes
                {
                    ClientFullName = s.LastName + " " + s.FirstName + " - " + s.Phone1,
                    ClientId = s.ClientId,

                }).OrderBy(s => s.ClientFullName).ToList();
        }

        public void InsertClient(ClientInLessonCreate value)
        {
            _context.AppClientInLessons.Add(new AppClientInLesson
            {
                ClientId = value.ClientId,
                LessonId = value.LessonId,
                JoinDate = DateTime.Now
            });

            _context.SaveChanges();
        }

        public void RemoveClientFromLesson(int clientId, int lessonId)
        {
            var entity = _context.AppClientInLessons.Where(x => x.ClientId == clientId && x.LessonId == lessonId).First();
            _context.AppClientInLessons.Remove(entity);
            _context.SaveChanges();
        }

        private bool IsSubscriptionValid(AppSubscription sub)
        {
            return sub.StartDate <= DateTime.Now && sub.EndDate >= DateTime.Now;
        }

        public void UpdateClientInLesson(ClientInLessonsCreate model)
        {
            var entitis = _context.AppClientInLessons.Where(x => x.ClientId == model.ClientId).ToList();
            _context.AppClientInLessons.RemoveRange(entitis);
            _context.SaveChanges();

            foreach (var item in model.LessonsId)
            {
                _context.AppClientInLessons.Add(new AppClientInLesson
                {
                    ClientId = model.ClientId,
                    LessonId = item,
                    JoinDate = DateTime.Now,
                });
            }

            _context.SaveChanges();


            // _con.Execute(@"
            // INSERT INTO App_lesson
            //     (Name
            //     ,Day
            //     ,InstructorId
            //     ,MaxMember)
            //VALUES 
            //     (@Name
            //     ,@Day
            //     ,@InstructorId
            //     ,@MaxMember)", model);
        }

    }
}
