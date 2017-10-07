using MySch.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MySch.Bll.Entity
{
    public class BllEdu : BllEntity<TEdu>
    {
        public string ID { get; set; }

        public string IDS { get; set; }

        [DisplayName("学制代码")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{1}$", ErrorMessage = "{0}：用1位数字设置；")]
        public int Value { get; set; }


        [DisplayName("学制名称")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{3,10}$", ErrorMessage = "{0}：3-10个中文字符；")]
        public string Name { get; set; }

        [DisplayName("是否启用")]
        public bool Fixed { get; set; }

        public string AccIDS { get; set; }
    }
}