using MySch.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MySch.Bll.Entity
{
    public class BllGrade : BllEntity<TGrade>
    {
        public string ID { get; set; }

        public string IDS { get; set; }

        [DisplayName("分级编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "{0}：用16位数字设置；")]
        public string StepIDS { get; set; }

        [DisplayName("年度编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "{0}：用12位数字设置；")]
        public string YearIDS { get; set; }

        [DisplayName("学制编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "{0}：用10位数字设置；")]
        public string EduIDS { get; set; }

        [DisplayName("是否能分班")]
        public bool CanFeng { get; set; }

        [DisplayName("交换用的人数")]
        public int TakeNum { get; set; }

        [DisplayName("公共关系模式")]
        public bool GoneModel { get; set; }

        [DisplayName("公共关系列表")]
        public string GoneList { get; set; }

        public string AccIDS { get; set; }
    }
}