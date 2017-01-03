﻿using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySch.Bll.Entity
{
    public class BllEdu : BllEntity<AEdu>
    {
        public string ID { get; set; }

        public string IDS { get; set; }

        [DisplayName("学制代码")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{2}$", ErrorMessage = "{0}：用2位数字设置；")]
        public string Value { get; set; }


        [DisplayName("学制名称")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{3,10}$", ErrorMessage = "{0}：3-10个中文字符；")]
        public string Name { get; set; }

        [DisplayName("是否启用")]
        public bool Fixed { get; set; }

        public string AccIDS { get; set; }
    }
}