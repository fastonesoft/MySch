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
    public class BllYear : BllEntity<TYear>
    {
        [DisplayName("级")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "{0}：为4位数字！")]
        public int IDS { get; set; }
        public string ID { get; set; }
        public bool Fixed { get; set; }

        //创建新的BLL实体
        public BllYear(int aid)
        {
            IDS = aid;
            ID = Guid.NewGuid().ToString("N");
            Fixed = false;
        }


    }
}