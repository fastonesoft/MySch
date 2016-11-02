using MySch.Bll.Entity;
using MySch.Bll.View;
using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MySch.Bll.Model
{
    public class XueAdd
    {
        [DisplayName("年级编号")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{18}$", ErrorMessage = "{0}：18位数学；")]
        public string GradeIDS { get; set; }

        [DisplayName("班级")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{20}$", ErrorMessage = "{0}：20位数学；")]
        public string BanIDS { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,10}$", ErrorMessage = "{0}：2-10个汉字；")]
        public string Name { get; set; }

        [DisplayName("身份证号")]
        [RegularExpression(@"^$|^\d{17}[0-9X]$", ErrorMessage = "{0}：为18位数学与X的组合；")]
        public string CID { get; set; }

        [DisplayName("原班")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "{0}：为4位数学；")]
        public string OldBan { get; set; }

        [DisplayName("学生来源")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "{0}：用10位数学设置；")]
        public string ComeIDS { get; set; }

        [DisplayName("是否择校")]
        [Required(ErrorMessage = "{0}：不得为空")]
        public bool Choose { get; set; }

        public string ToAdd(ModelStateDictionary model)
        {
            try
            {
                if (!model.IsValid) throw new Exception("表示层：数据验证无法通过！");

                //查询学生信息
                var grade = VGrade.GetEntity(a => a.IDS == this.GradeIDS);
                //学生库记录编号
                var studs = DataCRUD<TStudent>.Entitys(a => a.StepIDS == grade.StepIDS);
                var studs_max = studs.Any() ? studs.Max(a => a.IDS) : grade.StepIDS + "0000";
                var studs_max_prev = grade.StepIDS;
                var studs_max_order = int.Parse(studs_max.Substring(studs_max.Length - 4, 4)) + 1;
                //一、学生库添加
                BllStudentIn stud = new BllStudentIn()
                {
                    ID = Guid.NewGuid().ToString("N"),
                    IDS = studs_max_prev + studs_max_order.ToString("D4"),
                    Name = this.Name,
                    CID = this.CID,
                    StepIDS = grade.StepIDS,
                    AccIDS = grade.AccIDS,
                };
                stud.ToAdd();
                //二、年度学生库添加
                var grades = DataCRUD<TGradeStud>.Entitys(a => a.GradeIDS == grade.IDS);
                var grades_max = grades.Any() ? grades.Max(a => a.IDS) : grade.IDS + "0000";
                var grades_max_prev = grade.IDS;
                var grades_max_order = int.Parse(grades_max.Substring(grades_max.Length - 4, 4)) + 1;
                BllGradeStud gradestud = new BllGradeStud()
                {
                    ID = Guid.NewGuid().ToString("N"),
                    IDS = grades_max_prev + grades_max_order.ToString("D4"),
                    GradeIDS = grade.IDS,
                    StudIDS = stud.IDS,
                    BanIDS = this.BanIDS,
                    OldBan = this.OldBan,
                    Choose = this.Choose,
                    ComeIDS = this.ComeIDS,
                    Fixed = false,
                    InSch = true,
                    OutIDS = null,
                };
                gradestud.ToAdd();

                //返回ID
                return gradestud.ID;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}