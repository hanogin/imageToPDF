using System;

namespace Service.DTO
{
    public class InstructorRes
    {
        public int InstructorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int? HourlyWage { get; set; }

        //public bool IsActive { get; set; }
    }

    public class InstructorCreate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int? HourlyWage { get; set; }
        //public bool IsActive { get; set; }
    }

    public class InstructorUpdate
    {
        public int InstructorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Cell { get; set; }
        public string Address { get; set; }
        public int? HourlyWage { get; set; }

        //public bool IsActive { get; set; }
    }


    public class InstructorHistoryRes
    {
        public int InstructorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public DateTime Date { get; set; }
    }
}
