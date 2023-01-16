using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class AppMaccabyReportHistory
    {
        public int MaccabyReportHistoryId { get; set; }
        public DateTime ReportMonth { get; set; }
        public int SubscriptionId { get; set; }
    }
}
