using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class TStep
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Graduated { get; set; }
        public bool CanRecruit { get; set; }
        public string PartIDS { get; set; }
        public string AccIDS { get; set; }
    }
}
