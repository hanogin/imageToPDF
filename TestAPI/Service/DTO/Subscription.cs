using System;

namespace Service.DTO
{
    public class SubscriptionCreate
    {
        public int ActionType { get; set; }
        public int ClientId { get; set; }
        public int ProgramId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Visit { get; set; }
        public int Price { get; set; }
        public string Comment { get; set; }
    }

    public class SubscriptionUpdate
    {
        public int SubscriptionId { get; set; }
        //public int ActionType { get; set; }
        public int ProgramId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Visit { get; set; }
        public int Price { get; set; }
        public string Comment { get; set; }
    }

    public class SubscriptionRes
    {
        public string ClientFullName { get; set; }
        public int SubscriptionId { get; set; }
        public DateTime ActionDate { get; set; }
        public int ActionType { get; set; }
        public int ClientId { get; set; }
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Visit { get; set; }
        public int Price { get; set; }
        public string Comment { get; set; }
        public int VisitPerWeek { get; set; }
        public int DaysLeft { get; set; }
        public int Visited { get; set; }
        public int VisitsLeft { get; set; }
        public string Status { get; set; }
        public int MonthLeft { get; set; }
        public string AlertType { get; set; }
    }

    public class SubscriptionActiveByClientRes
    {
        public int SubscriptionId { get; set; }
        public string ProgramName { get; set; }
    }
}
