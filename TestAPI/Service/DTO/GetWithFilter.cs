using System;

namespace Service.DTO
{
    public class GetWithFilter
    {
        public int? ClientId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? LessonId { get; set; }
    }
}
