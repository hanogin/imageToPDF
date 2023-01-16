using DAL.Context;
using DAL.Entities;
using Serilog;
using Service.DTO;
using Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Service
{
    public class VisitService
    {
        private readonly SportiveContext _context;
        private readonly IDapperBaseRepository _con;
        private readonly ILogger _logger;

        public VisitService(
        IDapperBaseRepository dapper,
        SportiveContext context,
        ILogger logger)
        {
            _context = context;
            _con = dapper;
            _logger = logger;
        }

        public List<VisitRes> GetForReport(VisitReportReq value)
        {
            var visitQuery = _context.AppVisits.AsQueryable();

            visitQuery = visitQuery.Where(x => x.Date >= value.StartDate && x.Date <= value.EndDate &&
                                         (value.LessonId.HasValue && x.LessonId == value.LessonId.Value || !value.LessonId.HasValue) &&
                                         (value.InstructorId.HasValue && x.InstructorId == value.InstructorId.Value || !value.InstructorId.HasValue));

            return visitQuery
                 .GroupBy(c => new { c.Date, c.LessonId })
                 .Select(x => new VisitRes
                 {
                     VisitId = x.First().VisitId,
                     ClientId = x.First().ClientId,
                     Date = x.First().Date,
                     Day = Util.GetLetterOfDay((int)x.First().Date.Date.DayOfWeek),
                     ProgramName = x.First().Subscription.Program.Name,
                     LessonName = x.First().Lesson.Name,
                     LessonTime = x.First().Lesson.Time,
                     InstructorFullName = x.First().Instructor.FirstName + " " + x.First().Instructor.LastName,
                     IsTempInstructor = x.First().IsTempInstructor,
                     HourlyWage = x.First().Instructor.HourlyWage
                 }).ToList();
        }
        public List<VisitRes> GetAll()
        {
            return _context.AppVisits.Select(x => new VisitRes
            {
                VisitId = x.VisitId,
                ClientId = x.ClientId,
                Date = x.Date,
                Day = Util.GetLetterOfDay((int)x.Date.Date.DayOfWeek),
                ProgramName = x.Subscription.Program.Name,
                LessonName = x.Lesson.Name,
                InstructorFullName = x.Lesson.Instructor.FirstName + " " + x.Lesson.Instructor.LastName,
            }).ToList();

            //            return _con.Query<VisitRes>(@"
            //SELECT 
            //    visit.visitId,
            //    visit.ClientId,
            //    client.FirstName || ' ' || client.LastName AS ClientFullName,
            //    visit.date,
            //     (
            //           SELECT day
            //             FROM T_Days
            //            WHERE DaysId = strftime('%w', visit.date) 
            //     )
            //       day,
            //       prog.name AS programName,

            //       less.name AS lessonName,
            //       (inst.firstName || ' ' || inst.lastName) AS instructorFullName,
            //       visit.comment
            //FROM App_Visit visit
            //LEFT JOIN App_Client client ON client.CLientId = visit.ClientId
            //LEFT JOIN App_Subscription sub ON visit.subscriptionId
            //LEFT JOIN App_Program prog ON sub.ProgramId = sub.ProgramId
            //LEFT JOIN App_lesson less ON less.LessonId = visit.LessonId
            //LEFT JOIN App_Instructor inst ON inst.InstructorId = less.instructorId");
        }

        public List<VisitRes> GetByFilter(GetWithFilter model)
        {
            var query = _context.AppVisits.AsQueryable();

            if (model.ClientId.HasValue)
            {
                query = query.Where(x => x.ClientId == model.ClientId.Value);
            }


            if (!model.StartDate.HasValue && !model.EndDate.HasValue && !model.ClientId.HasValue)
            {
                query = query.Where(x => x.Date >= DateTime.Now.AddDays(-7));

                query = query.Where(x => x.Date <= DateTime.Now);
            }

            if (model.StartDate.HasValue)
            {
                query = query.Where(x => x.Date.Date >= model.StartDate.Value.Date);
            }

            if (model.EndDate.HasValue)
            {
                query = query.Where(x => x.Date.Date <= model.EndDate.Value.Date);
            }


            if (model.LessonId.HasValue)
            {
                query = query.Where(x => x.LessonId <= model.LessonId.Value);
            }

            return query.Select(x => new VisitRes
            {
                VisitId = x.VisitId,
                ClientId = x.ClientId,
                ClientFullName = x.Client.FirstName + " " + x.Client.LastName,
                Date = x.Date,
                Day = Util.GetLetterOfDay((int)x.Date.Date.DayOfWeek),
                ProgramName = x.Subscription.Program.Name,
                LessonName = x.Lesson.Name,
                LessonTime = x.Lesson.Time,
                InstructorFullName = x.Lesson.Instructor.FirstName + " " + x.Lesson.Instructor.LastName,
                SubsId = x.SubscriptionId,
                Comment = x.IsTempInstructor ? "מ'מ " + x.Instructor.FirstName + " " + x.Instructor.LastName : null,
                LessonId = x.LessonId
            }).ToList();

            //            return _con.Query<VisitRes>(@"
            //SELECT 
            //    visit.visitId,
            //    visit.ClientId,
            //    client.FirstName || ' ' || client.LastName AS ClientFullName,
            //    visit.date,
            //     (
            //           SELECT day
            //             FROM T_Days
            //            WHERE DaysId = strftime('%w', visit.date) 
            //     )
            //       day,
            //       prog.name AS programName,

            //       less.name AS lessonName,
            //       (inst.firstName || ' ' || inst.lastName) AS instructorFullName,
            //       visit.comment
            //FROM App_Visit visit
            //LEFT JOIN App_Client client ON client.CLientId = visit.ClientId
            //LEFT JOIN App_Subscription sub ON visit.subscriptionId
            //LEFT JOIN App_Program prog ON sub.ProgramId = sub.ProgramId
            //LEFT JOIN App_lesson less ON less.LessonId = visit.LessonId
            //LEFT JOIN App_Instructor inst ON inst.InstructorId = less.instructorId
            //  WHERE (@ClientId IS NULL OR visit.ClientId = @ClientId) 
            //        AND (@StartDate IS NULL OR visit.Date >= @StartDate)
            //        AND (@EndDate IS NULL OR visit.Date <= @EndDate)");
        }

        public bool Create(VisitCreate model)
        {
            var v = new GetWithFilter
            {
                ClientId = model.ClientId,
                EndDate = model.Date,
                StartDate = model.Date,
                LessonId = model.LessonId,
            };

            var visit = GetByFilter(v);
            if (visit.Count > 0)
            {
                return false;
            }

            var instructorId = _context.AppLessons.Where(x => x.LessonId == model.LessonId).First().InstructorId;
            _context.AppVisits.Add(new AppVisit
            {
                ClientId = model.ClientId,
                SubscriptionId = model.SubscriptionId,
                LessonId = model.LessonId,
                InstructorId = instructorId,
                Date = model.Date,
            });

            _context.SaveChanges();
            return true;
            //_con.Execute(@"INSERT INTO App_Visit (ClientId, subscriptionId, lessonId, date, comment) 
            //              VALUES (@ClientId, @SubscriptionId, @LessonId, @Date, @Comment)", model);
        }

        public void Update(VisitUpdate model)
        {
            //            var query = @"
            //UPDATE App_Visit
            //SET 
            //    lessonId = @LessonId,
            //    date = @Date,
            //    comment = @Comment
            //WHERE VisitId = @id";
            //               _con.Execute(query, model);

            var entity = _context.AppVisits.Where(x => x.VisitId == model.Id).First();

            entity.SubscriptionId = model.SubscriptionId;
            entity.LessonId = model.LessonId;
            entity.Date = model.Date;

            _context.SaveChanges();
        }

        public void Delete(int visitId)
        {
            //_con.Execute("DELETE FROM App_Visit WHERE VisitId = @VisitId", new { VisitId = visitId });

            var entity = _context.AppVisits.Where(x => x.VisitId == visitId).First();

            _context.AppVisits.Remove(entity);

            _context.SaveChanges();
        }


        public void CreateGroupPresence(VisitGroupPresenceCreate model)
        {
            List<AppVisit> visit = new List<AppVisit>();



            foreach (var client in model.Clients)
            {
                // If it's one time visit
                if (!client.SubscriptionId.HasValue)
                {
                    // Add subs
                    var entity = new AppSubscription
                    {
                        ClientId = client.ClientId,
                        ProgramId = 15,
                        Visit = 1,
                        Price = 40,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        ActionDate = DateTime.Now
                    };

                    _context.AppSubscriptions.Add(entity);

                    _context.SaveChanges();

                    // Add account
                    _context.AppAccounts.Add(new AppAccount
                    {
                        ClientId = client.ClientId,
                        SubscriptionId = entity.SubscriptionId,
                        Amount = -40,
                    });

                    _context.SaveChanges();

                    client.SubscriptionId = entity.SubscriptionId;
                }

                visit.Add(new AppVisit
                {
                    ClientId = client.ClientId,
                    SubscriptionId = client.SubscriptionId.Value,
                    Date = model.Date,
                    LessonId = model.LessonId,
                    InstructorId = model.TempInstructorId.HasValue ? model.TempInstructorId.Value : _context.AppLessons.Where(x => x.LessonId == model.LessonId).First().InstructorId,
                    IsTempInstructor = model.TempInstructorId.HasValue ? true : false, // For addd ממ in comment
                });

            }
            _context.AppVisits.AddRange(visit);
            _context.SaveChanges();

            //_con.Execute(@"INSERT INTO App_Visit (ClientId, subscriptionId, lessonId, date, comment) 
            //              VALUES (@ClientId, @SubscriptionId, @LessonId, @Date, @Comment)", model);
        }

        //public static IQueryable<TSource> DistinctBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        //{
        //    return source.GroupBy(keySelector).Select(x => x.FirstOrDefault());
        //}
    }
}
