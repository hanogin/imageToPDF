using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class TClientFileType
    {
        public TClientFileType()
        {
            AppClientFiles = new HashSet<AppClientFile>();
        }

        public int ClientFileTypeId { get; set; }
        public string ClientFileTypeDesc { get; set; } = null!;

        public virtual ICollection<AppClientFile> AppClientFiles { get; set; }
    }
}
