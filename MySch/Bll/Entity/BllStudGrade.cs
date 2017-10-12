using MySch.Bll.View;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MySch.Bll.Entity
{
    public class BllStudGrade : BllEntity<StudGrade>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string GradeIDS { get; set; }
        public string StudIDS { get; set; }
        public string StudCode { get; set; }
        public string BanIDS { get; set; }
        public string OldBan { get; set; }
        public bool Choose { get; set; }
        public string ComeIDS { get; set; }
        public Nullable<System.DateTime> ComeTime { get; set; }
        public string GroupID { get; set; }
        public bool Fixed { get; set; }
        public Nullable<int> Score { get; set; }
        public string OutIDS { get; set; }
        public Nullable<System.DateTime> OutTime { get; set; }
        public bool InSch { get; set; }
    }


    public class BllStudBack : BllEntity<StudGrade>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string GradeIDS { get; set; }

        [DisplayName("班级")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{20}$", ErrorMessage = "{0}：用20位数字设置；")]
        public string BanIDS { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,10}$", ErrorMessage = "{0}：2-10个中文字符；")]
        public string StudName { get; set; }

        [DisplayName("学生来源")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "{0}：用10位数字设置；")]
        public string ComeIDS { get; set; }

        //主要修改下面几个属性
        public bool InSch { get; set; }
        public bool CanReturn { get; set; }
    }

    public class BllStudBacks : BllEntity<StudGrade>
    {
        public string ID { get; set; }
        public string IDS { get; set; }

        //主要修改下面几个属性
        public bool InSch { get; set; }
        public string OutIDS { get; set; }
        public Nullable<System.DateTime> OutTime { get; set; }

        //全部回校（临时不在校的学生）
        public static string Backs(IEnumerable<VStudOut> entitys)
        {
            try
            {
                int count = 0;
                foreach (var entity in entitys)
                {
                    if (entity.OutName == "临时")
                    {
                        var back = new BllStudBacks
                        {
                            ID = entity.ID,
                            IDS = entity.IDS,
                            InSch = true,
                            OutIDS = null,
                            OutTime = null,
                        };
                        back.ToUpdate();
                        count++;
                    }
                }
                return string.Format("选中 {0} 人，实际回校 {1} 人。", entitys.Count(), count);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    public class BllStudDrop : BllEntity<StudGrade>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string PartIDS { get; set; }
        public string GradeIDS { get; set; }
        public string StudIDS { get; set; }

        [DisplayName("班级")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{20}$", ErrorMessage = "{0}：用20位数字设置；")]
        public string BanIDS { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,10}$", ErrorMessage = "{0}：2-10个中文字符；")]
        public string StudName { get; set; }

        [DisplayName("校区分级")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "{0}：用16位数字设置；")]
        public string StepIDS { get; set; }

        //主要修改下面几个属性
        public bool InSch { get; set; }
        public string OutIDS { get; set; }
        public DateTime OutTime { get; set; }
    }

    public class BllStudGradeOut : BllEntity<StudGrade>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string PartIDS { get; set; }
        public string GradeIDS { get; set; }

        [DisplayName("班级")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{20}$", ErrorMessage = "{0}：用20位数字设置；")]
        public string BanIDS { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,10}$", ErrorMessage = "{0}：2-10个中文字符；")]
        public string StudName { get; set; }

        [DisplayName("校区分级")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "{0}：用16位数字设置；")]
        public string StepIDS { get; set; }

        [DisplayName("学生去向")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "{0}：用10位数字设置；")]
        public string OutIDS { get; set; }

        [DisplayName("离校时间")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{4}(-\d{2}){2}$", ErrorMessage = "{0}：用10位数字设置；")]
        public string OutTimeIn { get; set; }

        //主要修改下面几个属性
        public bool InSch { get; set; }
        public DateTime OutTime { get; set; }
    }

    public class BllBanChange : BllEntity<StudGrade>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string GradeIDS { get; set; }

        [DisplayName("班级")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{20}$", ErrorMessage = "{0}：用20位数字设置；")]
        public string BanIDS { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,10}$", ErrorMessage = "{0}：2-10个中文字符；")]
        public string StudName { get; set; }
    }


}