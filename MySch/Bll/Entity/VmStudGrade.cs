using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySch.Bll.Entity
{
    public class VmStudGradeGroupID : BllEntity<StudGrade>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string GroupID { get; set; }
    }

    public class VmStudGradeFixed : BllEntity<StudGrade>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public bool Fixed { get; set; }
    }
}