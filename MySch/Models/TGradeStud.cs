using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class TGradeStud
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string GradeIDS { get; set; }
        public string StudIDS { get; set; }
        public string BanIDS { get; set; }
        public string OldBan { get; set; }
        public bool Choose { get; set; }
        public string AddIDS { get; set; }
        public string GroupID { get; set; }
        public bool Fixed { get; set; }
        public Nullable<int> Score { get; set; }
    }
}
