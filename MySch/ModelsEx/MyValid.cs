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
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,30}$", ErrorMessage = "{0}：2-30个中文字符")]
        public string Name { get; set; }

        [DisplayName("用户密码")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^[a-zA-Z0-9\.]{6,32}$", ErrorMessage = "{0}：6-32个英文字母、数字")]
        public string Pwd { get; set; }

        [DisplayName("用户冻结")]
        public bool Fixed { get; set; }

        public DateTime RegTime { get; set; }
        public string Parent { get; set; }
    }

    //学籍查询请求所需数据格式
    public class StudQueValid
    {
        [DisplayName("姓名")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,10}$", ErrorMessage = "{0}：2-10个中文字符")]
        public string Name { get; set; }

        [DisplayName("身份证号")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{17}[0-9X]$", ErrorMessage = "{0}：为18位数学大写X的组合")]
        public string ID { get; set; }
    }

    //学籍编号
    public class StudEditValid
    {
        public string GD { get; set; }

        [DisplayName("姓名")]
        public string Name { get; set; }

        [DisplayName("报名编号")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{2}(00[1-9]|0[1-9]\d|[1-9]\d\d)-\d{4}$", ErrorMessage = "{0}：XXXXX-XXXX")]
        public string studNo { get; set; }

        [DisplayName("备注")]
        [StringLength(20, ErrorMessage="{0}：长度不能超过20")]
        public string Memo { get; set; }

        [DisplayName("是否择校")]
        public bool schChoose { get; set; }
    }
    
    //学籍手动添加
    public class StudManuValid
    {
        [DisplayName("身份证号")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{17}[0-9X]$", ErrorMessage = "{0}：为18位数学大写X的组合")]
        public string ID { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,10}$", ErrorMessage = "{0}：2-10个中文字符")]
        public string Name { get; set; }

        [DisplayName("毕业学校")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{4,20}$", ErrorMessage = "{0}：4-20个中文字符")]
        public string fromSch { get; set; }

        [DisplayName("毕业年级")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^小学(五|六)年级$", ErrorMessage = "{0}：小学X年级（中文）")]
        public string fromGrade { get; set; }
    }

    //学籍注册
    public class StudRegValid
    {
        public string GD { get; set; }

        [DisplayName("姓名")]
        public string Name { get; set; }

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

        [DisplayName("家庭地址")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [StringLength(50, ErrorMessage = "{0}：长度不能超过50")]
        [RegularExpression(@"^姜堰区[#-a-zA-Z0-9\u4e00-\u9fa5]{10,30}$|^[\u4e00-\u9fa5]{2,10}镇[#-a-zA-Z0-9\u4e00-\u9fa5]{10,30}$", ErrorMessage = "{0}：姜堰区X、X镇X")]
        public string Home { get; set; }

        [DisplayName("户籍地址")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [StringLength(50, ErrorMessage = "{0}：长度不能超过50")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,10}省[\u4e00-\u9fa5]{2,10}市[\u4e00-\u9fa5]{2,10}(市|区|县)[#-a-zA-Z0-9\u4e00-\u9fa5]{10,30}$|^[\u4e00-\u9fa5]{2,10}市[\u4e00-\u9fa5]{2,10}(市|区|县)[#-a-zA-Z0-9\u4e00-\u9fa5]{10,30}|[\u4e00-\u9fa5]{2,10}(市|区|县)[#-a-zA-Z0-9\u4e00-\u9fa5]{10,30}$|^[\u4e00-\u9fa5]{2,10}镇[#-a-zA-Z0-9\u4e00-\u9fa5]{10,30}$", ErrorMessage = "{0}：X省X市X(市区县)X、X市X(市区县)X、姜堰区X、姜堰区X镇X")]
        public string Permanent { get; set; }
    }

}