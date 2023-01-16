using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class TClientType
    {
        public TClientType()
        {
            AppClients = new HashSet<AppClient>();
        }

        public int ClientTypeId { get; set; }
        public string ClientTypeDesc { get; set; } = null!;

        public virtual ICollection<AppClient> AppClients { get; set; }
    }
}
