using MySch.Core;
using MySch.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class ViSchGrade:BllBase<ViewSchGrade>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string PartIDS { get; set; }
        public string StepIDS { get; set; }
        public string TreeName { get; set; }
        public string Name { get; set; }
        public string YearName { get; set; }
        public bool Graduated { get; set; }
        public bool IsCurrent { get; set; }
        public bool CanRecruit { get; set; }
        public bool CanFeng { get; set; }
        public int TakeNum { get; set; }
        public bool GoneModel { get; set; }
        public string GoneList { get; set; }

    }
}