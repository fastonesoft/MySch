using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class UploadFile
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string FileType { get; set; }
        public string UploadType { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string Author { get; set; }
        public string Memo { get; set; }
    }
}
