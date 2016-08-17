using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class TAcc
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public System.DateTime RegTime { get; set; }
        public bool Fixed { get; set; }
        public string Parent { get; set; }
    }
}
