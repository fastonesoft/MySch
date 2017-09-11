using MySch.Bll;
using MySch.Core;
using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySch.Mvvm.Wall
{
    public class VmWxAccFilt : BllEntity<WxAccFilter>
    {
        public string ID { get; set; }

        [DisplayName("用户手机")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^1(3\d|4[57]|5[0-35-9]|8\d|7[6-9])\d{8}$", ErrorMessage = "{0}：号码有误")]
        public string IDS { get; set; }

        [DisplayName("用户名称")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,4}$", ErrorMessage = "{0}：2-4个中文字符")]
        public string Name { get; set; }

        [DisplayName("集团短号")]
        [RegularExpression(@"^\d{4,6}$", ErrorMessage = "{0}：号码有误")]
        public string Mobils { get; set; }
    }

    public class VqWxAccFilt : BllBase<WxAccFilter>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public string Mobils { get; set; }
    }
}