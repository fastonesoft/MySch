using MySch.Core;
using MySch.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class ViSchStep:BllBase<ViewSchStep>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string PartIDS { get; set; }
        public string Name { get; set; }
        public string PartName { get; set; }
        public string StepName { get; set; }
        public bool Graduated { get; set; }
        public bool CanRecruit { get; set; }
        public string AccIDS { get; set; }
    }
}