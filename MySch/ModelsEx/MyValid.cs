using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySch.Models
{

    [MetadataType(typeof(AccValid))]
    public partial class TAcc { }
    public class AccValid
    {
        [DisplayName("用户帐号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^admin$|^\d{8,20}$|^[a-zA-Z]{8,32}$", ErrorMessage = "{0}：为数字、字母组")]
        public string ID { get; set; }

        public string GD { get; set; }

        [DisplayName("用户名称")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,30}$", ErrorMessage = "{0}：2-30个中文字符；")]
        public string Name { get; set; }

        [DisplayName("用户密码")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^[a-zA-Z0-9\.]{6,32}$", ErrorMessage = "{0}：6-32个英文字母、数字。")]
        public string Pwd { get; set; }

        [DisplayName("用户冻结")]
        public bool Fixed { get; set; }

        public DateTime RegTime { get; set; }
        public string Parent { get; set; }
    }


    public class StudRegValid
    {
        [DisplayName("姓名")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,30}$", ErrorMessage = "{0}：2-10个中文字符；")]
        public string Name { get; set; }

        [DisplayName("联系电话")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^1(3[0-9]|4[57]|5[0-35-9]|8[0-9]|70)\d{8}$", ErrorMessage = "{0}：为11位手机号；")]
        public string Tel { get; set; }

        [DisplayName("身份证号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{17}[0-9xX]$", ErrorMessage = "{0}：为18位数学字母组合")]
        public string ID { get; set; }
    }


}