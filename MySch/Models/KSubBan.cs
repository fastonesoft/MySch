using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class KSubBan
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string BanIDS { get; set; }
        public string SubGradeIDS { get; set; }
        public string AccIDS { get; set; }
        public bool IsMaster { get; set; }
    }
}
