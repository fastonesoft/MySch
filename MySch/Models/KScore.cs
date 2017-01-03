using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class KScore
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string KStudIDS { get; set; }
        public string KaoIDS { get; set; }
        public string SubIDS { get; set; }
        public Nullable<double> Value { get; set; }
        public Nullable<int> BanIndex { get; set; }
        public Nullable<int> GradeIndex { get; set; }
        public Nullable<int> GroupIndex { get; set; }
        public Nullable<int> TotalIndex { get; set; }
    }
}
