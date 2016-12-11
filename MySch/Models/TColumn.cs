using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class TColumn
    {
        public string IDS { get; set; }
        public string Name { get; set; }
        public string Html { get; set; }
        public bool Fixed { get; set; }
        public string PageIDS { get; set; }
        public virtual TPage TPage { get; set; }
    }
}
