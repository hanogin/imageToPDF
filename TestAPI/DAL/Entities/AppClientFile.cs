using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class AppClientFile
    {
        public int ClientFileId { get; set; }
        public DateTime ActionDate { get; set; }
        public int ClientId { get; set; }
        public int ClientFileTypeId { get; set; }
        public byte[] File { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string? FileType { get; set; }
        public string? Comment { get; set; }

        public virtual TClientFileType ClientFileType { get; set; } = null!;
    }
}
