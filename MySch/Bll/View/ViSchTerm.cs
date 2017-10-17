using MySch.Core;
using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class ViSchTerm: BllBase<ViewSchTerm>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public bool IsCurrent { get; set; }
        public string YearIDS { get; set; }
        public string AccIDS { get; set; }
    }
}