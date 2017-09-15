using MySch.Bll;
using MySch.Core;
using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MySch.Mvvm.Wall
{
    public class VmWxAction : BllEntity<WxAction>
    {
        public string ID { get; set; }

        [DisplayName("活动编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "{0}：8位数字，例：2017XXXX")]
        public string IDS { get; set; }

        [DisplayName("活动名称")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "{0}：字符长度在{2} - {1}之间")]
        public string Name { get; set; }

        [DisplayName("当前活动")]
        public bool IsCurrent { get; set; }

        [DisplayName("检测当前")]
        public bool NeedCheck { get; set; }


        //清除当前
        public void ClearCurrent()
        {
            try
            {
                var entitys = VmWxAction.GetEntitys<VmWxAction>(a => a.IsCurrent);
                foreach(var entity in entitys)
                {
                    entity.IsCurrent = false;
                    entity.ToUpdate();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }

    public class VqWxAction : BllBase<WxAction>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public bool IsCurrent { get; set; }
        public bool NeedCheck { get; set; }
    }
}