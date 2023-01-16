

using System;
using System.Collections.Generic;

namespace Service.DTO
{
    public class VisitRes
    {
        public int VisitId { get; set; }
        public int LessonId { get; set; }
        public int ClientId { get; set; }
        public string ClientFullName { get; set; }
        public DateTime Date { get; set; }
        public string Day { get; set; }
        public string ProgramName { get; set; }
        public string LessonName { get; set; }
        public TimeSpan? LessonTime { get; set; }
        public string InstructorFullName { get; set; }
        public int SubsId { get; set; }
        public string Comment { get; set; }
        public bool IsTempInstructor { get; set; }
        public int? HourlyWage { get; set; }
    }

    public class VisitCreate
    {
        public int ClientId { get; set; }
        public int SubscriptionId { get; set; }
        public int LessonId { get; set; }
        public DateTime Date { get; set; }
    }

    public class VisitUpdate
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int SubscriptionId { get; set; }
        public int LessonId { get; set; }
        public DateTime Date { get; set; }
    }

    public class VisitGroupPresenceCreate
    {
        public List<ClientForGroupPresence> Clients { get; set; }
        public int LessonId { get; set; }
        public DateTime Date { get; set; }
        public int? TempInstructorId { get; set; }
        public bool? IsOneTimeSub { get; set; }
    }

    public class ClientForGroupPresence
    {
        public int ClientId { get; set; }
        public int? SubscriptionId { get; set; }
    }
}
