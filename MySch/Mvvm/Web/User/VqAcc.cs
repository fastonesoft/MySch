using MySch.Bll;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Mvvm.Web.User
{
    public class VqAccBan: BllBase<TAcc>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public bool Passed { get; set; }
        public bool Fixed { get; set; }
        public string ParentID { get; set; }
    }
}