using System;
using System.Collections.Generic;

namespace Service.DTO
{
    public class LessonRes
    {
        public int LessonId { get; set; }
        public string Name { get; set; }
        public int DayId { get; set; }
        public string DayName { get; set; }
        public int instructorId { get; set; }
        public int MaxMember { get; set; }
        public bool IsActive { get; set; }
        public string InstructorFullName { get; set; }
        public string DateTime { get; set; }
    }

    public class LessonCreate
    {
        public string Name { get; set; }
        public int DayId { get; set; }
        public int InstructorId { get; set; }
        public int MaxMember { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsActive { get; set; }
    }

    public class LessonUpdate
    {
        public int LessonId { get; set; }
        public string Name { get; set; }
        public int DayId { get; set; }
        public int InstructorId { get; set; }
        public int MaxMember { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
