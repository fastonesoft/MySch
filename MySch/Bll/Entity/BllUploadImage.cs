using MySch.Models;

namespace MySch.Bll.Entity
{
    public class BllUploadImage : BllBase<WxUploadFile>
    {
        public string ID { get; set; }

        public string IDS { get; set; }

        public string FileType { get; set; }
    }
}