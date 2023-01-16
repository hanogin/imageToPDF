using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class VSubscription
    {
        public int ClientId { get; set; }
        public string ProgramName { get; set; } = null!;
        public int NumberOfVisitPerWeek { get; set; }
        public int? DaysLeft { get; set; }
        public int? MonthLeft { get; set; }
        public int? Visited { get; set; }
        public int? VisitsLeft { get; set; }
        public int? Status { get; set; }
    }
}
