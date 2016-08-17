using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll
{
    public class BllLogin : Bll<TLogin>
    {
        public string ID { get; set; }
        public int IDS { get; set; }
        public string Brower { get; set; }
        public string IP { get; set; }
        public System.DateTime loginTime { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public string loginMsg { get; set; }
    }
}