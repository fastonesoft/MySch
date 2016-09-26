using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.Entity
{
    public class BllGrade : BllEntity<TGrade>
    {
        public string ID { get; set; }

        public string IDS { get; set; }

        [DisplayName("校区分级")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "{0}：用16位数字设置；")]
        public string PartStepIDS { get; set; }

        [DisplayName("年度编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "{0}：用12位数字设置；")]
        public string YearIDS { get; set; }

        [DisplayName("学制编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "{0}：用10位数字设置；")]
        public string EduIDS { get; set; }

        public string AccIDS { get; set; }
    }
}