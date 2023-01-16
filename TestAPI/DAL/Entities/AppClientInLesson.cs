using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class AppClientInLesson
    {
        public int ClientInLessonId { get; set; }
        public int ClientId { get; set; }
        public int LessonId { get; set; }
        public DateTime JoinDate { get; set; }

        public virtual AppClient Client { get; set; } = null!;
        public virtual AppLesson Lesson { get; set; } = null!;
    }
}
