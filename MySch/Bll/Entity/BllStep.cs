﻿using MySch.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MySch.Bll.Entity
{
    public class BllStep : BllEntity<TStep>
    {
        public string ID { get; set; }

        public string IDS { get; set; }

        [DisplayName("分级名称")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"[\d\u4e00-\u9fa5]{4,10}$", ErrorMessage = "{0}：4-10个数字、汉字；")]
        public string Name { get; set; }

        [DisplayName("分级代码")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "{0}：用6位数字设置；")]
        public string Value { get; set; }

        [DisplayName("是否毕业")]
        public bool Graduated { get; set; }

        [DisplayName("是否招生")]
        public bool CanRecruit { get; set; }

        [DisplayName("校区名称")]
        public string PartIDS { get; set; }

        public string AccIDS { get; set; }
    }
}