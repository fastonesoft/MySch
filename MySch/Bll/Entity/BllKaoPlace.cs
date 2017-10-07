using MySch.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MySch.Bll.Entity
{
    public class BllKaoPlace : BllEntity<KaoPlace>
    {
        public string ID { get; set; }

        public string IDS { get; set; }

        [DisplayName("考场编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{2}$", ErrorMessage = "{0}：用2位数字设置；")]
        public string PlaceNo { get; set; }

        [DisplayName("是否启用")]
        public bool Fixed { get; set; }
    }
}