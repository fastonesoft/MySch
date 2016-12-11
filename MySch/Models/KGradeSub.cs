using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class KGradeSub
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string GradeIDS { get; set; }
        public string SubIDS { get; set; }
        public int Value { get; set; }
        public bool Scoring { get; set; }
    }
}
