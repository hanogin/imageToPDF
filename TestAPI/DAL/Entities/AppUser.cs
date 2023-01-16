using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class AppUser
    {
        public AppUser()
        {
            AppAccounts = new HashSet<AppAccount>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int UserRoleId { get; set; }

        public virtual TUserRole UserRole { get; set; } = null!;
        public virtual ICollection<AppAccount> AppAccounts { get; set; }
    }
}
