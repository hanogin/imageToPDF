using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class TempImpoirtClientTable
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? IdentityId { get; set; }
        public string? Address { get; set; }
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? CalientTypeDesc { get; set; }
        public string? KupatHolimDesc { get; set; }
        public string? Email { get; set; }
    }
}
