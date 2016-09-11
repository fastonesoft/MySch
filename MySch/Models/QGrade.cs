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
        public string Name { get; set; }
        public string GradeName { get; set; }
        public string PartStepName { get; set; }
        public string YearName { get; set; }
        public bool Graduated { get; set; }
        public bool IsCurrent { get; set; }
    }
}
