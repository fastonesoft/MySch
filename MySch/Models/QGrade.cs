using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class QGrade
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string PartStepIDS { get; set; }
        public string YearIDS { get; set; }
        public string EduIDS { get; set; }
        public string AccIDS { get; set; }
        public string PartIDS { get; set; }
        public string StepIDS { get; set; }
        public string Name { get; set; }
        public string TreeName { get; set; }
        public string EduName { get; set; }
        public string PartName { get; set; }
        public string YearName { get; set; }
        public bool Graduated { get; set; }
        public bool IsCurrent { get; set; }
        public bool CanRecruit { get; set; }
    }
}
