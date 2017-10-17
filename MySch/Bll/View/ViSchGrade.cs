using MySch.Core;
using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class ViSchGrade : BllBase<ViewSchGrade>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string StepIDS { get; set; }
        public string YearIDS { get; set; }
        public string EduIDS { get; set; }
        public bool CanFeng { get; set; }
        public int TakeNum { get; set; }
        public bool GoneModel { get; set; }
        public string GoneList { get; set; }
        public string PartIDS { get; set; }
        public string AccName { get; set; }
        public string PartName { get; set; }
        public string StepName { get; set; }
        public string EduName { get; set; }
        public string TreeName { get; set; }
        public string Name { get; set; }
        public bool CurrentYear { get; set; }
    }
}