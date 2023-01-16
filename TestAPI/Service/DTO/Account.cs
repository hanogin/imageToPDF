using System;

namespace Service.DTO
{
    public class AccountRes
    {
        public int AccountId { get; set; }
        public DateTime ActionDate { get; set; }
        public int ClientId { get; set; }
        public string ClientFullName { get; set; }
        public int SubscriptionId { get; set; }
        public string ProgramName { get; set; }
        public string PaymentMethodDesc { get; set; }
        public string ActionTypeDesc { get; set; }
        public int Amount { get; set; }
        public string ActionByUserName { get; set; }
    }

    public class AccountAddDeposit
    {
        public int ClientId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
