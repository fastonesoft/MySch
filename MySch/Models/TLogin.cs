using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class TLogin
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string IP { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public string Brower { get; set; }
        public string LoginMsg { get; set; }
        public System.DateTime LoginTime { get; set; }
    }
}
