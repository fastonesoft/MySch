using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class QBan
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public int Num { get; set; }
        public string GradeIDS { get; set; }
        public string MasterIDS { get; set; }
        public string GroupIDS { get; set; }
        public string AccIDS { get; set; }
        public string PartStepIDS { get; set; }
        public string YearIDS { get; set; }
        public string EduIDS { get; set; }
        public string Name { get; set; }
        public string TreeName { get; set; }
        public string DataGridName { get; set; }
        public string MasterName { get; set; }
        public string GroupName { get; set; }
        public bool Graduated { get; set; }
    }
}
