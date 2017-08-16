using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Models
{
    public partial class ARoleAction
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public bool IsMenu { get; set; }
        public string RoleTypeIDS { get; set; }
    }
}