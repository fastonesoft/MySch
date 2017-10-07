using MySch.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MySch.Bll.Entity
{
    public class BllAcc : BllEntity<TAcc>
    {
        [DisplayName("用户帐号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{8,11}$|^\d{17}[\dxX]$|^[a-zA-Z]{5,20}$", ErrorMessage = "{0}：为20以内的数字、字母")]
        public string IDS { get; set; }

        [DisplayName("用户名称")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,20}$", ErrorMessage = "{0}：2-20个中文字符")]
        public string Name { get; set; }

        [DisplayName("用户密码")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^[a-zA-Z0-9\.]{6,32}$", ErrorMessage = "{0}：6-32个英文字母、数字")]
        public string Pwd { get; set; }

        [DisplayName("用户冻结")]
        public bool Fixed { get; set; }

        public string ID { get; set; }
        public DateTime RegTime { get; set; }
        public string ParentID { get; set; }
    }
}