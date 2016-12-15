using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySch.Bll.Entity
{
    public class BllColumn : BllEntity<TColumn>
    {
        public string ID { get; set; }

        [DisplayName("栏目编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^[a-zA-Z]{8,20}$", ErrorMessage = "{0}：为20以内的字母")]
        public string IDS { get; set; }

        [DisplayName("栏目名称")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{4,10}$", ErrorMessage = "{0}：4-10个中文字符")]
        public string Name { get; set; }

        [DisplayName("栏目脚本")]
        [Required(ErrorMessage = "{0}不得为空；")]
        public string Html { get; set; }
        
        [DisplayName("是否启用")]
        [Required(ErrorMessage = "{0}不得为空；")]
        public bool Fixed { get; set; }

        [DisplayName("页面编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        public string PageIDS { get; set; }

    }
}