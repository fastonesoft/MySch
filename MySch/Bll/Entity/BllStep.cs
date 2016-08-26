using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySch.Bll.Entity
{
    public class BllStep : BllEntity<TStep>
    {
        public string ID { get; set; }

        [DisplayName("分级编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "{0}：用12位数字设置；")]
        public string IDS { get; set; }

        [DisplayName("分级名称")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "{0}：用4位数字设置；")]
        public int Name { get; set; }

        [DisplayName("是否毕业")]
        public bool Fixed { get; set; }

    }
}