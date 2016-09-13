using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class QTerm
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public bool IsCurrent { get; set; }
        public string YearIDS { get; set; }
        public string SemesterIDS { get; set; }
        public string AccIDS { get; set; }
        public string Name { get; set; }
    }
}
