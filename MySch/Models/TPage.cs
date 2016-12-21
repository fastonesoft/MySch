using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class TPage
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public bool Bootup { get; set; }
        public string Html { get; set; }
        public bool Fixed { get; set; }
        public string ParentID { get; set; }
    }
}
