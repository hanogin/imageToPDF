using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class VClientVisit
    {
        public int VisitId { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public string? Day { get; set; }
        public string ProgramName { get; set; } = null!;
        public string LessonName { get; set; } = null!;
        public string InstructorFullName { get; set; } = null!;
        public string? Comment { get; set; }
    }
}
