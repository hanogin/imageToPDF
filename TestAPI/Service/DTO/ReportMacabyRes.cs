using System;

namespace Service.DTO
{
    public class ReportMacabyRes
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityId { get; set; }
        public string Phone1 { get; set; }
        public string Address { get; set; }

        public int NumberOfmonth { get; set; }
        public int ReportCount { get; set; }
        public int ClientId { get; set; }
        public int SubscriptionId { get; set; }
        public string ProgramName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}


