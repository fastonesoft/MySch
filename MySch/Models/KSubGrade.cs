using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class KSubGrade
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string GradeIDS { get; set; }
        public string SubIDS { get; set; }
        public int DefaultValue { get; set; }
        public bool DefaultScoring { get; set; }
    }
}
