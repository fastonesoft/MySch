﻿using MySch.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MySch.Bll.Entity
{
    public class BllAdminPage : BllEntity<AdminPage>
    {
        public string ID { get; set; }

        [DisplayName("页面编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^[a-zA-Z]{6,20}[0-9]{1,3}$", ErrorMessage = "{0}：为20以内的字母")]
        public string IDS { get; set; }

        [DisplayName("页面名称")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{4,10}$", ErrorMessage = "{0}：4-10个中文字符")]
        public string Name { get; set; }

        [DisplayName("启动页")]
        public bool Bootup { get; set; }

        [DisplayName("页面代码")]
        [Required(ErrorMessage = "{0}不得为空；")]
        public string Html { get; set; }

        [DisplayName("脚本样式")]
        public string Script { get; set; }

        [DisplayName("是否禁用")]
        [Required(ErrorMessage = "{0}不得为空；")]
        public bool Fixed { get; set; }

        [DisplayName("父亲编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        public string ParentID { get; set; }
    }
}