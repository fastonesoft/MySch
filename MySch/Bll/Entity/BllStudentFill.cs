using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MySch.Bll.Entity
{
    public class BllStudentFill : BllEntity<Student>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Mobil2 { get; set; }
        public string Birth { get; set; }
        public string Home { get; set; }
    }
}