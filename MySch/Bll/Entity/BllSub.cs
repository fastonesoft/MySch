using MySch.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MySch.Bll.Entity
{
    public class BllSub : BllEntity<TSub>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string AccIDS { get; set; }

        [DisplayName("学科编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{2}$", ErrorMessage = "{0}：用2位数字设置；")]
        public string Value { get; set; }

        [DisplayName("学科名称")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,5}$", ErrorMessage = "{0}：2-5个中文字符；")]
        public string Name { get; set; }

        [DisplayName("学科简称")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{1}$", ErrorMessage = "{0}：1个中文字符；")]
        public string SName { get; set; }

        [DisplayName("是否禁用")]
        public bool Fixed { get; set; }
    }
}