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
    public class VmWxPrize : BllEntity<WxPrize>
    {
        public string ID { get; set; }

        [DisplayName("中奖编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^(0[1-9])|([1-9]\d)$", ErrorMessage = "{0}：2位数字，01 - 99")]
        public string IDS { get; set; }

        [DisplayName("中奖名称")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "{0}：字符长度在{2} - {1}之间")]
        public string Name { get; set; }

        [DisplayName("奖品数量")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^[1-9]\d?$", ErrorMessage = "{0}：数值范围，1 - 99")]
        public int Num { get; set; }

    }

    public class VqWxPrize : BllBase<WxPrize>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public int Num { get; set; }
    }
}