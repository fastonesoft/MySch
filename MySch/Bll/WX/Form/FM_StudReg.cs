using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySch.Bll.WX.Form
{
    public class FM_StudReg
    {
        [DisplayName("身份证号")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{17}[\dxX]$", ErrorMessage = "{0}：是18位的二代证")]
        public string idc { get; set; }

        [DisplayName("手机号码")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^1(3[0-9]|4[57]|5[0-35-9]|8[0-9]|7[6-8])\d{8}$", ErrorMessage = "{0}：为11位手机号")]
        public string mobil { get; set; }
    }
}