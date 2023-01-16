using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class AppLesson
    {
        public AppLesson()
        {
            AppClientInLessons = new HashSet<AppClientInLesson>();
            AppVisits = new HashSet<AppVisit>();
        }

        public int LessonId { get; set; }
        public string Name { get; set; } = null!;
        public int DayId { get; set; }
        public int InstructorId { get; set; }
        public int MaxMember { get; set; }
        public TimeSpan? Time { get; set; }
        public bool IsActive { get; set; }

        public virtual TDay Day { get; set; } = null!;
        public virtual AppInstructor Instructor { get; set; } = null!;
        public virtual ICollection<AppClientInLesson> AppClientInLessons { get; set; }
        public virtual ICollection<AppVisit> AppVisits { get; set; }
    }
}
