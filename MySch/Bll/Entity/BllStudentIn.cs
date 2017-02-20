using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MySch.Bll.Entity
{
    public class BllStudentIn : BllEntity<Student>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public string IDC { get; set; }
        public string StepIDS { get; set; }
        public bool IsProblem { get; set; }
        public string Mobil1 { get; set; }
        public string Mobil2 { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Home { get; set; }
        public string Birth { get; set; }
        public bool Checked { get; set; }
        public bool CanModify { get; set; }
        public string Memo { get; set; }
        public string AccIDS { get; set; }
        public string OpenID { get; set; }
    }
}