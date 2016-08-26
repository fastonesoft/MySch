using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.View
{
    public class QllPartStep: BllBase<QPartStep>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string PartIDS { get; set; }
        public string StepIDS { get; set; }
        public string PartName { get; set; }
        public int StepName { get; set; }
    }
}