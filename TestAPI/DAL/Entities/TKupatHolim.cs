using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class TKupatHolim
    {
        public TKupatHolim()
        {
            AppClients = new HashSet<AppClient>();
        }

        public int KupatHolimId { get; set; }
        public string KupatHolimDesc { get; set; } = null!;

        public virtual ICollection<AppClient> AppClients { get; set; }
    }
}
