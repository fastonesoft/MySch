using MySch.Bll;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Mvvm.School.Stud
{
    public class VqBanPhotos: BllBase<QrWxStudentUpload>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string FileType { get; set; }
        public string UploadType { get; set; }
        public DateTime CreateTime { get; set; }
        public string IDC { get; set; }
        public string Name { get; set; }
        public string StudSex { get; set; }
        public string GradeIDS { get; set; }
        public string BanIDS { get; set; }
        public string Num { get; set; }
        public bool InSch { get; set; }
        public int? Score { get; set; }
        public string StudGradeID { get; set; }
    }
}