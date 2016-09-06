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
    public class BllStudentIn : BllEntity<TStudent>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public string FromSch { get; set; }
        public string FromGrade { get; set; }
        public string NationID { get; set; }
        public string ReadState { get; set; }
        public bool IsProblem { get; set; }
        public string StudNo { get; set; }
        public bool SchChoose { get; set; }
        public string Memo { get; set; }
        public string Mobil1 { get; set; }
        public string Mobil2 { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Home { get; set; }
        public string Permanent { get; set; }
        public bool Reged { get; set; }
        public string OpenID { get; set; }

        //学生导入
        public static IEnumerable<BllStudentIn> Import()
        {
            var studs = DataCRUD<TStudReg>.Entitys(a => a.IsProblem == false);
            foreach(var stud in studs)
            {

            }

            return null;
        }
    }
}