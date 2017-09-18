using MySch.Models;
using System;

namespace MySch.Bll.Entity
{
    public class BllImageUpload : BllEntity<WxUploadFile>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string FileType { get; set; }
        public string UploadType { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class BllImageDelete: BllEntity<WxUploadFile>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
    }
}