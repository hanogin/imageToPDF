using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class AppSubscription
    {
        public AppSubscription()
        {
            AppAccounts = new HashSet<AppAccount>();
            AppVisits = new HashSet<AppVisit>();
        }

        public int SubscriptionId { get; set; }
        public DateTime ActionDate { get; set; }
        public int? ActionTypeId { get; set; }
        public int ClientId { get; set; }
        public int ProgramId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Visit { get; set; }
        public int Price { get; set; }
        public string? Comment { get; set; }

        public virtual AppClient Client { get; set; } = null!;
        public virtual AppProgram Program { get; set; } = null!;
        public virtual ICollection<AppAccount> AppAccounts { get; set; }
        public virtual ICollection<AppVisit> AppVisits { get; set; }
    }
}
