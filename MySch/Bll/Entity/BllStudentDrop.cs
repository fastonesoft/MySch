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
    public class BllStudentDrop : BllEntity<TStudent>
    {
        public string ID { get; set; }
        public string IDS { get; set; }

        //修改：校区分级属性，降级
        public string StepIDS { get; set; }
    }
}