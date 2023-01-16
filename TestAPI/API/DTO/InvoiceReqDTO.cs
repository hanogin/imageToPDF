namespace API.DTO
{
    public class InvoiceReqDTO
    {
        public int InvoiceId { get; set; }
        public string FullName { get; set; }
        public string Date { get; set; }
        public int DonateSum { get; set; }
        public int Balance { get; set; }
        public string Address { get; set; }
        public string Apartment { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
    }
}
