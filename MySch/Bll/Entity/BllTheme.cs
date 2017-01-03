using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySch.Bll.Entity
{
    public class BllTheme : BllEntity<ATheme>
    {
        public string ID { get; set; }

        [DisplayName("模板编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^[a-zA-Z-]{6,20}$", ErrorMessage = "{0}：为20以内的字母")]
        public string IDS { get; set; }

        [DisplayName("模板名称")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{4,10}$", ErrorMessage = "{0}：4-10个中文字符")]
        public string Name { get; set; }

        [DisplayName("当前模板")]
        public bool IsCurrent { get; set; }
    }
}