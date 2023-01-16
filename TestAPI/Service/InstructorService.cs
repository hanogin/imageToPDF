using DAL.Context;
using DAL.Entities;
using Serilog;
using Service.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class InstructorService
    {
        private readonly SportiveContext _context;
        private readonly IDapperBaseRepository _con;
        private readonly ILogger _logger;

        public InstructorService(
        SportiveContext context,
        IDapperBaseRepository dapper,
        ILogger logger)
        {
            _context = context;
            _con = dapper;
            _logger = logger;
        }

        public List<InstructorRes> GetAll()
        {
            //return _con.Query<InstructorRes>("SELECT * FROM App_Instructor");

            return _context.AppInstructors.Select(x => new InstructorRes
            {
                InstructorId = x.InstructorId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Cell = x.Cell,
                Phone = x.Phone,
                Email = x.Email,
                Address = x.Address,
                HourlyWage = x.HourlyWage,
                //IsActive = x.IsActive
            }).ToList();
        }

        public void Create(InstructorCreate model)
        {
            _context.AppInstructors.Add(new AppInstructor
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Cell = model.Cell,
                Email = model.Email,
                Address = model.Address,
                HourlyWage = model.HourlyWage,
                //IsActive = model.IsActive
            });

            _context.SaveChanges();

            // _con.Execute(@"
            // INSERT INTO App_Instructor 
            //     (FirstName
            //     ,LastName
            //     ,Phone
            //     ,Cell
            //     ,Email)
            //VALUES 
            //     (@FirstName
            //     ,@LastName
            //     ,@Phone
            //     ,@Cell
            //     ,@Email)", model);
        }

        public void Update(InstructorUpdate model)
        {
            //    _con.Execute(@"
            //Update App_Instructor 
            //    SET FirstName = @FirstName, 
            //        LastName = @LastName, 
            //        Phone = @Phone, 
            //        Cell = @Cell, 
            //        Email = @Email
            //    WHERE InstructorId = @InstructorId", model);

            var entity = _context.AppInstructors.Where(x => x.InstructorId == model.InstructorId).First();

            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Phone = model.Phone;
            entity.Cell = model.Cell;
            entity.Email = model.Email;
            entity.Address = model.Address;
            entity.HourlyWage= model.HourlyWage;
            //model.IsActive = model.IsActive;

            _context.SaveChanges();
        }

        //public List<InstructorHistoryRes> GetHistory(int instructorId)
        //{

        //}

        //public List<InstructorHistoryRes> GetHistory()
        //{

        //}
    }
}
