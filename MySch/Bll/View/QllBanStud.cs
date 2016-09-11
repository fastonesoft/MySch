using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class QllBanStud : BllBase<QGradeStud>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string BanIDS { get; set; }
        public string StudIDS { get; set; }
        public string GradeIDS { get; set; }
        public string GradeName { get; set; }
        public bool Graduated { get; set; }
        public string StudID { get; set; }
        public string StudName { get; set; }
        public string StudNo { get; set; }
        public string StudSex { get; set; }
    }
}