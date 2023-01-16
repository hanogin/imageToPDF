using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class VisitReportReq
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? LessonId { get; set; }
        public int? InstructorId { get; set; }
    }
}
