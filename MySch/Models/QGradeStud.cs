using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class QGradeStud
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string GradeIDS { get; set; }
        public string StudIDS { get; set; }
        public string StudCode { get; set; }
        public string BanIDS { get; set; }
        public string OldBan { get; set; }
        public bool Choose { get; set; }
        public string ComeIDS { get; set; }
        public string GroupID { get; set; }
        public bool Fixed { get; set; }
        public Nullable<int> Score { get; set; }
        public string BanName { get; set; }
        public string DataGridName { get; set; }
        public bool Graduated { get; set; }
        public string StudName { get; set; }
        public string StudSex { get; set; }
        public string CID { get; set; }
        public string ComeName { get; set; }
        public bool Checked { get; set; }
    }
}
