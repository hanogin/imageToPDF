using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class AppAccount
    {
        public int AccountId { get; set; }
        public DateTime ActionDate { get; set; }
        public int ClientId { get; set; }
        public int? SubscriptionId { get; set; }
        public int? PaymentTypeId { get; set; }
        public int Amount { get; set; }
        public string? Comment { get; set; }
        public int? UserId { get; set; }

        public virtual AppClient Client { get; set; } = null!;
        public virtual TPaymentType? PaymentType { get; set; }
        public virtual AppSubscription? Subscription { get; set; }
        public virtual AppUser? User { get; set; }
    }
}
