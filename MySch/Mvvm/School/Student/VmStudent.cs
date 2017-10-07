using MySch.Bll;
using MySch.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MySch.Mvvm.School.Student
{
    public class VmStudentEdit: BllEntity<Stud>
    {
        public string ID { get; set; }
        public string IDS { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,10}$", ErrorMessage = "{0}：2-10个汉字；")]
        public string Name { get; set; }

        [DisplayName("身份证号")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^$|^\d{17}[0-9X]$", ErrorMessage = "{0}：为18位数字与X的组合；")]
        public string IDC { get; set; }

        [DisplayName("手机号码")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^1(3[0-9]|4[57]|5[0-35-9]|8[0-9]|7[6-9])\d{8}$", ErrorMessage = "{0}：为11位手机号")]
        public string Mobil1 { get; set; }

        [DisplayName("联系电话")]
        [RegularExpression(@"^1(3[0-9]|4[57]|5[0-35-9]|8[0-9]|7[6-9])\d{8}$", ErrorMessage = "{0}：为11位手机号")]
        public string Mobil2 { get; set; }
    }
}