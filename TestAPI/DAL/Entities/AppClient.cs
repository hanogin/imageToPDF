using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class AppClient
    {
        public AppClient()
        {
            AppAccounts = new HashSet<AppAccount>();
            AppClientInLessons = new HashSet<AppClientInLesson>();
            AppSubscriptions = new HashSet<AppSubscription>();
            AppVisits = new HashSet<AppVisit>();
        }

        public int ClientId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? IdentityId { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? KupatHolimId { get; set; }
        public int? ClientTypeId { get; set; }
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime? JoinDate { get; set; }

        public virtual TClientType? ClientType { get; set; }
        public virtual TKupatHolim? KupatHolim { get; set; }
        public virtual ICollection<AppAccount> AppAccounts { get; set; }
        public virtual ICollection<AppClientInLesson> AppClientInLessons { get; set; }
        public virtual ICollection<AppSubscription> AppSubscriptions { get; set; }
        public virtual ICollection<AppVisit> AppVisits { get; set; }
    }
}
