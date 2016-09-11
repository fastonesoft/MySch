using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class QllGradeStud : BllBase<QGradeStud>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string GradeIDS { get; set; }
        public string StudIDS { get; set; }
        public string BanIDS { get; set; }
        public string OldBan { get; set; }
        public bool Choose { get; set; }
        public string ComeIDS { get; set; }
        public string GroupID { get; set; }
        public bool Fixed { get; set; }
        public Nullable<int> Score { get; set; }
        public string GradeName { get; set; }
        public bool Graduated { get; set; }
        public string StudName { get; set; }
        public string StudSex { get; set; }
        public string CID { get; set; }
        public string ComeName { get; set; }
        public bool Checked { get; set; }
    }
}