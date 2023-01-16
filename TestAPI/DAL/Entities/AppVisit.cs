using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class AppVisit
    {
        public int VisitId { get; set; }
        public int ClientId { get; set; }
        public int SubscriptionId { get; set; }
        public int LessonId { get; set; }
        public DateTime Date { get; set; }
        public int InstructorId { get; set; }
        public bool IsTempInstructor { get; set; }

        public virtual AppClient Client { get; set; } = null!;
        public virtual AppInstructor Instructor { get; set; } = null!;
        public virtual AppLesson Lesson { get; set; } = null!;
        public virtual AppSubscription Subscription { get; set; } = null!;
    }
}
