using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class TDay
    {
        public TDay()
        {
            AppLessons = new HashSet<AppLesson>();
        }

        public int DaysId { get; set; }
        public string? DaysDesc { get; set; }

        public virtual ICollection<AppLesson> AppLessons { get; set; }
    }
}
