using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class VStudent 
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public string CID { get; set; }
        public string PartStepIDS { get; set; }
        public string OutIDS { get; set; }
        public bool Checked { get; set; }
        public string AccIDS { get; set; }
        public string OpenID { get; set; }
        public string OutName { get; set; }
        public string PartStepName { get; set; }
    }
}