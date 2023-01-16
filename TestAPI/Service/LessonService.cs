using DAL.Context;
using DAL.Entities;
using Serilog;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class LessonService
    {
        private readonly SportiveContext _context;
        private readonly IDapperBaseRepository _con;
        private readonly ILogger _logger;

        public LessonService(
         SportiveContext context,
        IDapperBaseRepository dapper,
        ILogger logger)
        {
            _con = dapper;
            _logger = logger;
            _context = context;

        }

        public List<LessonRes> GetAll()
        {
            //            return _con.Query<LessonRes>(@"
            //SELECT 
            //    less.LessonId,
            //    less.name,
            //    less.day,
            //    less.maxMember, 
            //    less.instructorId, 
            //    inst.firstName || ' ' ||  inst.lastName AS instructorFullName
            //FROM App_lesson less
            //LEFT JOIN App_Instructor inst ON inst.InstructorId = less.instructorId");

            return _context.AppLessons.Select(x => new LessonRes
            {
                LessonId = x.LessonId,
                Name = x.Name,
                DayId = x.DayId,
                DayName = x.Day.DaysDesc,
                instructorId = x.InstructorId,
                InstructorFullName = x.Instructor.FirstName + " " + x.Instructor.LastName,
                MaxMember = x.MaxMember,
                DateTime = x.Time.HasValue ? x.Time.Value.ToString("hh':'mm") : "",
                IsActive = x.IsActive
            }).ToList().OrderByDescending(x => x.IsActive).ThenBy(x => x.DayId).ThenBy(x => x.DateTime).ToList();
        }

        public List<LessonRes> GetClientLesson(int clientId)
        {
            return _context.AppLessons
                .Where(x => x.AppClientInLessons.Any(x => x.ClientId == clientId))
                .Select(x => new LessonRes
                {
                    LessonId = x.LessonId,
                    Name = x.Name,
                    DayId = x.DayId,
                    DayName = x.Day.DaysDesc,
                    instructorId = x.InstructorId,
                    InstructorFullName = x.Instructor.FirstName + " " + x.Instructor.LastName,
                    MaxMember = x.MaxMember,
                    DateTime = x.Time.HasValue ? x.Time.Value.ToString("hh':'mm") : "",
                    IsActive = x.IsActive 
                }).ToList();

            //            return _con.Query<LessonRes>(@"
            //SELECT 
            //    less.LessonId,
            //    less.name,
            //    less.day,
            //    less.maxMember, 
            //    less.instructorId, 
            //    inst.firstName || ' ' ||  inst.lastName AS instructorFullName
            //FROM App_lesson less
            //LEFT JOIN App_Instructor inst ON inst.InstructorId = less.instructorId
            //WHERE EXIST(SELECT FROM App_ClientInLesson cil WHERE cil.ClientId = @ClientId AND cli.LessonId = less.LessonId)", new { ClientId  = clientId});
        }


        public void Create(LessonCreate model)
        {
            _context.AppLessons.Add(new AppLesson
            {
                Name = model.Name,
                DayId = model.DayId,
                InstructorId = model.InstructorId,
                MaxMember = model.MaxMember,
                Time = model.DateTime.TimeOfDay,
                IsActive = model.IsActive,
            });

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

        public void Update(LessonUpdate model)
        {
            var entity = _context.AppLessons.Where(x => x.LessonId == model.LessonId).First();

            entity.Name = model.Name;
            entity.DayId = model.DayId;
            entity.InstructorId = model.InstructorId;
            entity.MaxMember = model.MaxMember;
            entity.Time = model.DateTime.TimeOfDay;
            entity.IsActive = model.IsActive;

            _context.SaveChanges();

            //_con.Execute(@"
            //        Update App_lesson
            //            SET Name = @Name,
            //                Day = @Day,
            //                InstructorId = @InstructorId,
            //                MaxMember = @MaxMember
            //            WHERE LessonId = @LessonId", model);
        }


    }
}
