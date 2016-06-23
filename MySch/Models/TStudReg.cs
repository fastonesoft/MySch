using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class TStudReg
    {
        public string ID { get; set; }
        public string GD { get; set; }
        public string Name { get; set; }
        public string fromSch { get; set; }
        public int fromClass { get; set; }
        public string fromPhoto { get; set; }
        public bool schChoose { get; set; }
        public string Memo { get; set; }
    }
}
