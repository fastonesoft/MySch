﻿using MySch.Dal;
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
    public class BllBanChange : BllEntity<StudGrade>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string GradeIDS { get; set; }

        [DisplayName("班级")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{20}$", ErrorMessage = "{0}：用20位数学设置；")]
        public string BanIDS { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,10}$", ErrorMessage = "{0}：2-10个中文字符；")]
        public string StudName { get; set; }
    }
}