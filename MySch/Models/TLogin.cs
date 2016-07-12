using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class TLogin
    {
        public long ID { get; set; }
        public string Brower { get; set; }
        public string IP { get; set; }
        public System.DateTime loginTime { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public string loginMsg { get; set; }
    }
}
