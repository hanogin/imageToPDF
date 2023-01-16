using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class AppProgram
    {
        public AppProgram()
        {
            AppSubscriptions = new HashSet<AppSubscription>();
        }

        public int ProgramId { get; set; }
        public string Name { get; set; } = null!;
        public int NumberOfmonth { get; set; }
        public int NumberOfVisitPerWeek { get; set; }
        public int NumberOfDays { get; set; }
        public int Price { get; set; }

        public virtual ICollection<AppSubscription> AppSubscriptions { get; set; }
    }
}
