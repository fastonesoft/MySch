using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySch.Bll.Entity
{
    public class BllStudentMove : BllEntity<StudGradeMove>
    {
        public string ID { get; set; }

        public string IDS { get; set; }

        public string BanIDS { get; set; }

        public string OwnerIDS { get; set; }
    }
}