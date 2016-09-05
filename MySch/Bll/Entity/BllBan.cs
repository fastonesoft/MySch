using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySch.Bll.Entity
{
    public class BllBan : BllEntity<TBan>
    {
        public string ID { get; set; }

        public string IDS { get; set; }

        [DisplayName("班级名称")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{1,2}$", ErrorMessage = "{0}：为1-2位数字！")]
        public int Num { get; set; }

        [DisplayName("年级编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "{0}：用16位数字设置；")]
        public string GradeIDS { get; set; }

        [DisplayName("班主任")]
        public string MasterIDS { get; set; }

        [DisplayName("班级分组负责人")]
        public string GroupIDS { get; set; }

        public string AccIDS { get; set; }

    }
}