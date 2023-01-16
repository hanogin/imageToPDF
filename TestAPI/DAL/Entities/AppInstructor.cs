using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class AppInstructor
    {
        public AppInstructor()
        {
            AppLessons = new HashSet<AppLesson>();
            AppVisits = new HashSet<AppVisit>();
        }

        public int InstructorId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Cell { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public int? HourlyWage { get; set; }

        public virtual ICollection<AppLesson> AppLessons { get; set; }
        public virtual ICollection<AppVisit> AppVisits { get; set; }
    }
}
