using MySch.Bll;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySch.Mvvm.School.Student
{
    public class VmStudGradeGroupID : BllEntity<StudGrade>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string GroupID { get; set; }
    }

    public class VmStudGradeFixed : BllEntity<StudGrade>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public bool Fixed { get; set; }
    }

    public class VmStudGradeBan : BllEntity<StudGrade>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string BanIDS { get; set; }
        public bool Fixed { get; set; }
        public string GroupID { get; set; }
    }

    public class VmStudGradeBanWanted : BllEntity<StudGrade>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string BanIDS { get; set; }
        public bool Fixed { get; set; }
    }

    public class VmStudGradeBanGone : BllEntity<StudGrade>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string BanIDS { get; set; }
        public string GroupID { get; set; }
    }

    public class VmStudGradeBanInfor : BllEntity<TBan>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public bool IsAbs { get; set; }
        public bool SameSex { get; set; }
    }

    /// <summary>
    /// 学生分班二维交换数据格式
    /// </summary>
    public class VmStudGradeMove
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string BanIDS { get; set; }
        public string OwnerAccIDS { get; set; }
        public string Command { get; set; }
        public string ID2 { get; set; }
        public string IDS2 { get; set; }
        public string BanIDS2 { get; set; }
        public string OwnerAccIDS2 { get; set; }

        public string ToUrlString()
        {
            var res = string.Empty;
            var format = "{0}={1}&";
            res += string.Format(format, "ID", !string.IsNullOrEmpty(ID) ? ID : "null");
            res += string.Format(format, "IDS", !string.IsNullOrEmpty(IDS) ? IDS : "null");
            res += string.Format(format, "BanIDS", !string.IsNullOrEmpty(BanIDS) ? BanIDS : "null");
            res += string.Format(format, "OwnerAccIDS", !string.IsNullOrEmpty(OwnerAccIDS) ? OwnerAccIDS : "null");
            res += string.Format(format, "Command", !string.IsNullOrEmpty(Command) ? Command : "null");
            res += string.Format(format, "ID2", !string.IsNullOrEmpty(ID2) ? ID2 : "null");
            res += string.Format(format, "IDS2", !string.IsNullOrEmpty(IDS2) ? IDS2 : "null");
            res += string.Format(format, "BanIDS2", !string.IsNullOrEmpty(BanIDS2) ? BanIDS2 : "null");
            res += string.Format(format, "OwnerAccIDS2", !string.IsNullOrEmpty(OwnerAccIDS2) ? OwnerAccIDS2 : "null");
            //
            return Uri.EscapeDataString(res.Substring(0, res.Length - 1));
        }
    }
}