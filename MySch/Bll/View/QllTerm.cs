using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class QllTerm : BllBase<QTerm>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public bool IsCurrent { get; set; }
        public string YearIDS { get; set; }
        public string SemesterIDS { get; set; }
        public string AccIDS { get; set; }
        public string YearName { get; set; }
        public string TermName { get; set; }
    }
}