namespace Service.DTO
{
    public class ProgramPrRes
    {
        public int ProgramId { get; set; }
        public string Name { get; set; }
        public int NumberOfMonth { get; set; }
        public int NumberOfVisitPerWeek { get; set; }
        public int Price { get; set; }
        public int NumberOfDays { get; set; }

        //public bool IsActive { get; set; }
    }

    public class ProgramPrCreate
    {
        public string Name { get; set; }
        public int NumberOfMonth { get; set; }
        public int NumberOfVisitPerWeek { get; set; }
        public int Price { get; set; }
        public int NumberOfDays { get; set; }

        //public bool IsActive { get; set; }
    }

    public class ProgramPrUpdate
    {
        public int ProgramId { get; set; }
        public string Name { get; set; }
        public int NumberOfMonth { get; set; }
        public int NumberOfVisitPerWeek { get; set; }
        public int Price { get; set; }
        public int NumberOfDays { get; set; }

        //public bool IsActive { get; set; }
    }
}
