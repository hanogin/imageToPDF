using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class ClientWithSub
    {
        public int ClientId { get; set; }
        public string ClientFullName { get; set; } = null!;
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? FullNameWithPhone1 { get; set; }
        public string? Email { get; set; }
        public int? SubscriptionId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ProgramName { get; set; }
        public int? SubsStatusId { get; set; }
        public int? DaysLeft { get; set; }
        public int? Visited { get; set; }
        public int? VisitsLeft { get; set; }
    }
}
