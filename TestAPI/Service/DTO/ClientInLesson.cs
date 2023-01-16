using System.Collections.Generic;

namespace Service.DTO
{
    public class ClientInLesson
    {
    }

    public class ClientInLessonCreate
    {
        public int ClientId { get; set; }
        public int LessonId { get; set; }
    }

    public class ClientInLessonsCreate
    {
        public int ClientId { get; set; }
        public List<int>? LessonsId { get; set; }
    }
}
