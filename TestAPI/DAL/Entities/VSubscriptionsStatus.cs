using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class VSubscriptionsStatus
    {
        public int ClientId { get; set; }
        public int SubScriptionId { get; set; }
        public int? SubsStatusId { get; set; }
        public string? SubsStatusStyle { get; set; }
        public string? SubsStatusLabel { get; set; }
    }
}
