using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class TColumn
    {
        public string ID { get; set; }
        public string GD { get; set; }
        public string Name { get; set; }
        public string Html { get; set; }
        public string Txt { get; set; }
        public bool Fixed { get; set; }
        public string PageID { get; set; }
    }
}
