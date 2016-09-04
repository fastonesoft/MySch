using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class QllPartStep : BllBase<QPartStep>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string PartIDS { get; set; }
        public string StepIDS { get; set; }
        public string AccIDS { get; set; }
        public string Name { get; set; }
        public string PartName { get; set; }
        public string StepName { get; set; }
        public bool Graduated { get; set; }
    }
}