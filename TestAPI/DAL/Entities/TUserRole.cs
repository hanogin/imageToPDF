using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class TUserRole
    {
        public TUserRole()
        {
            AppUsers = new HashSet<AppUser>();
        }

        public int UserRoleId { get; set; }
        public string UserRoleDesc { get; set; } = null!;

        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}
