using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.Entity
{
    public class BllGradeStud : BllEntity<TGradeStud>
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
        public Nullable<System.DateTime> ComeTime { get; set; }
        public string GroupID { get; set; }
        public bool Fixed { get; set; }
        public Nullable<int> Score { get; set; }
        public string OutIDS { get; set; }
        public Nullable<System.DateTime> OutTime { get; set; }
        public bool InSch { get; set; }
    }
}