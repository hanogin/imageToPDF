using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class TPaymentType
    {
        public TPaymentType()
        {
            AppAccounts = new HashSet<AppAccount>();
        }

        public int PaymentTypeId { get; set; }
        public string PaymentTypeDesc { get; set; } = null!;

        public virtual ICollection<AppAccount> AppAccounts { get; set; }
    }
}
