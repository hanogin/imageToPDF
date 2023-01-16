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
    public class TestService
    {
        private readonly SportiveContext _context;
        private readonly IDapperBaseRepository _con;
        private readonly ILogger _logger;

        public TestService(
        IDapperBaseRepository dapper,
        SportiveContext context,
        ILogger logger)
        {
            _context = context;
            _con = dapper;
            _logger = logger;
        }

        // For keep sql server
        public void KeepServerALive()
        {
            _logger.Debug("KeepServerALive");
            _context.AppUsers.First();
        }
    }
}
