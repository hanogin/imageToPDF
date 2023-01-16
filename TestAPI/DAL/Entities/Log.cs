using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class Log
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? LogLevel { get; set; }
        public DateTime? Date { get; set; }
        public string? Exception { get; set; }
        public string? UserName { get; set; }
        public long? LogId { get; set; }
        public string? ClientIp { get; set; }
    }
}
