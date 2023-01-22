using API.Enum;

namespace API.DTO
{
    public class PersonDetailsDTO
    {
        public int id { get; set; }
        public int personIdNo { get; set; }
        public string personIdType { get; set; }
        public string name { get; set; }
        public string degree { get; set; }
        public string genderCode { get; set; }
        public string honorRoll { get; set; }
        public string degreeNameWithExt { get; set; }
        public string email { get; set; }
        public string phoneMobile { get; set; }
        public string phoneHome { get; set; }
        public string fullAddress { get; set; }
        public string cityName { get; set; }
        public string countryName { get; set; }
        public string streetName { get; set; }
        public string streetNo { get; set; }
        public string entrance { get; set; }
    }

    public class ReportDetailsDTO
    {
        public int balance { get; set; }
        public int totalAmount { get; set; }
        public bool viewBalance { get; set; } = true;
        public int currency { get; set; }
        public string operatorName { get; set; } 
        public string hebDateTitle { get; set; }

    }

    public class ReqDTO
    {
        public PrintOutTypeCodes printOutTypeCode { get; set; }
        public string entityCode { get; set; }
        public string serialNumber { get; set; }
        public ReportDetailsDTO reportDetails { get; set; }
        public PersonDetailsDTO personDetails { get; set; }
    }
}
