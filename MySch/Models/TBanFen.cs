using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class TBanFen
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string GradeOldIDS { get; set; }
        public string GradeNewIDS { get; set; }
        public string BanOldIDS { get; set; }
        public string BanNewIDS { get; set; }
        public Nullable<double> Total { get; set; }
    }
}
