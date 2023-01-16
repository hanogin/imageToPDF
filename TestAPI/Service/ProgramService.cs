using DAL.Context;
using DAL.Entities;
using Serilog;
using Service.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class ProgramService
    {
        private readonly SportiveContext _context;
        private readonly IDapperBaseRepository _con;
        private readonly ILogger _logger;

        public ProgramService(
         SportiveContext context,
        IDapperBaseRepository dapper,
        ILogger logger)
        {
            _con = dapper;
            _logger = logger;
            _context = context;

        }

        public List<ProgramPrRes> GetAll()
        {
            return _context.AppPrograms.Select(x => new ProgramPrRes
            {
                ProgramId = x.ProgramId,
                Name = x.Name,
                NumberOfMonth = x.NumberOfmonth,
                NumberOfVisitPerWeek = x.NumberOfVisitPerWeek,
                Price = x.Price,
                NumberOfDays = x.NumberOfDays
                //IsActive = x.IsActive
            }).ToList();
            //return _con.Query<ProgramPrRes>("SELECT * FROM App_Program");

        }

        public void Create(ProgramPrCreate model)
        {
            _context.AppPrograms.Add(new AppProgram
            {
                Name = model.Name,
                NumberOfmonth = model.NumberOfMonth,
                NumberOfVisitPerWeek = model.NumberOfVisitPerWeek,
                Price = model.Price,
                NumberOfDays= model.NumberOfDays
                //IsActive = model.IsActive
            });

            _context.SaveChanges();

            // _con.Execute(@"
            // INSERT INTO App_Program
            //     (Name
            //     ,NumberOfMonth
            //     ,NumberOfVisitPerWeek
            //     ,Price)
            //VALUES 
            //     (@Name
            //     ,@NumberOfMonth
            //     ,@NumberOfVisitPerWeek
            //     ,@Price)", model);
        }

        public void Update(ProgramPrUpdate model)
        {
            var entity = _context.AppPrograms.Where(x => x.ProgramId == model.ProgramId).First();

            entity.Name = model.Name;
            entity.NumberOfmonth = model.NumberOfMonth;
            entity.NumberOfVisitPerWeek = model.NumberOfVisitPerWeek;
            entity.Price = model.Price;
            entity.NumberOfDays = model.NumberOfDays;
            //entity.IsActive = model.IsActive;

            _context.SaveChanges();

            //_con.Execute(@"
            //        Update App_Program
            //            SET Name = @Name,
            //                NumberOfMonth = @NumberOfMonth,
            //                NumberOfVisitPerWeek = @NumberOfVisitPerWeek,
            //                Price = @Price
            //            WHERE ProgramId = @ProgramId", model);
        }
    }
}
