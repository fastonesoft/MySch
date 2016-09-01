using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class RllBan : BllBase<QBan>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public int Name { get; set; }
        public string GradeIDS { get; set; }
        public string MasterIDS { get; set; }
        public string GroupIDS { get; set; }
        public string AccIDS { get; set; }
        public string PartStepIDS { get; set; }
        public string PartIDS { get; set; }
        public string StepIDS { get; set; }
        public string YearIDS { get; set; }
        public string EduIDS { get; set; }
        public string BanName { get; set; }
        public string GradeName { get; set; }
        public string PartStepName { get; set; }
        public string PartName { get; set; }
        public string StepName { get; set; }
        public string EduName { get; set; }
        public string MasterName { get; set; }
        public string GroupName { get; set; }
    }
}