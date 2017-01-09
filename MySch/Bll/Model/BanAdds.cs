using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySch.Bll.Model
{
    public class BanAdds
    {
        [DisplayName("年级编号")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{18}$", ErrorMessage = "{0}：18位数字；")]
        public string GradeIDS { get; set; }
   
        [DisplayName("班级数量")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{1,2}$", ErrorMessage = "{0}：最大2位数字；")]
        public int Num { get; set; }
    }
}