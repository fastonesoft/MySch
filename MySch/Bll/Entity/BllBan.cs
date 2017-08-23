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
        [RegularExpression(@"^0[1-9]$|^[1-9]\d$", ErrorMessage = "{0}：为2位数字！")]
        public string Num { get; set; }

        [DisplayName("年级编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{18}$", ErrorMessage = "{0}：用18位数字设置；")]
        public string GradeIDS { get; set; }

        [DisplayName("班主任")]
        public string MasterIDS { get; set; }

        [DisplayName("调动人数")]
        [RangeAttribute(0, 99, ErrorMessage = "设定数值必须在{1}-{2}之间")]
        public int ChangeNum { get; set; }

        [DisplayName("调动分差")]
        [RangeAttribute(0, 20, ErrorMessage = "设定数值必须在{1}-{2}之间")]
        public int Differ { get; set; }

        [DisplayName("是否绝对值")]
        public bool IsAbs { get; set; }

        [DisplayName("只显固定")]
        public bool OnlyFixed { get; set; }

        [DisplayName("不参加分班")]
        public bool NotFeng { get; set; }

        public string AccIDS { get; set; }

    }
}