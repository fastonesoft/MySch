using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySch.Bll.Entity
{
    public class BllStudent : BllEntity<TStudent>
    {
        public string ID { get; set; }
        public string IDS { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,10}$", ErrorMessage = "{0}：2-10个中文字符")]
        public string Name { get; set; }

        [DisplayName("身份证号")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{17}[0-9X]$", ErrorMessage = "{0}：为18位数学大写X的组合")]
        public string CID { get; set; }

        [DisplayName("第一监护人")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,10}$", ErrorMessage = "{0}：2-10个中文字符")]
        public string Name1 { get; set; }

        [DisplayName("第二监护人")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^无$|^[\u4e00-\u9fa5]{2,10}$", ErrorMessage = "{0}：2-10个中文字符｜无")]
        public string Name2 { get; set; }

        [DisplayName("联系电话一")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^1(3[0-9]|4[57]|5[0-35-9]|8[0-9]|70)\d{8}$", ErrorMessage = "{0}：为11位手机号")]
        public string Mobil1 { get; set; }

        [DisplayName("联系电话二")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^无$|^1(3[0-9]|4[57]|5[0-35-9]|8[0-9]|70)\d{8}$", ErrorMessage = "{0}：为11位手机号｜无")]
        public string Mobil2 { get; set; }

        [DisplayName("户籍地址")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [StringLength(50, ErrorMessage = "{0}：长度不能超过50")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,10}省[\u4e00-\u9fa5]{2,10}市[\u4e00-\u9fa5]{2,10}(市|区|县)[#-a-zA-Z0-9\u4e00-\u9fa5]{10,20}$|^[\u4e00-\u9fa5]{2,10}市[\u4e00-\u9fa5]{2,10}(市|区|县)[#-a-zA-Z0-9\u4e00-\u9fa5]{10,20}|[\u4e00-\u9fa5]{2,10}(市|区|县)[#-a-zA-Z0-9\u4e00-\u9fa5]{10,20}$|^[\u4e00-\u9fa5]{2,10}镇[#-a-zA-Z0-9\u4e00-\u9fa5]{5,20}$", ErrorMessage = "{0}：X省X市X(市区县)X、X市X(市区县)X、姜堰区X、姜堰区X镇X")]
        public string Birth { get; set; }

        [DisplayName("家庭地址")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [StringLength(50, ErrorMessage = "{0}：长度不能超过50")]
        [RegularExpression(@"^姜堰区[#-a-zA-Z0-9\u4e00-\u9fa5]{10,20}$|^[\u4e00-\u9fa5]{2,10}镇[#-a-zA-Z0-9\u4e00-\u9fa5]{5,20}$", ErrorMessage = "{0}：姜堰区X、X镇X")]
        public string Home { get; set; }

        public bool Checked { get; set; }
        public bool Fixed { get; set; }
    }
}