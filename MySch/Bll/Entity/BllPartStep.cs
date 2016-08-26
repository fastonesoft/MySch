using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySch.Bll.Entity
{
    public class BllPartStep : BllEntity<TPartStep>
    {
        public string ID { get; set; }

        [DisplayName("校区分级")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "{0}：用14位数字设置；")]
        public string IDS { get; set; }

        [DisplayName("校区编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        public string PartIDS { get; set; }

        [DisplayName("分级编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        public string StepIDS { get; set; }

        public string AccIDS { get; set; }
    }
}