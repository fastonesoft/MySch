using MySch.Bll.View;
using MySch.Dal;
using MySch.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace MySch.Bll.Entity
{
    public class BllStud : BllEntity<Stud>
    {
        public string ID { get; set; }

        [DisplayName("学生编号")]
        public string IDS { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,10}$", ErrorMessage = "{0}：2-10个中文字符")]
        public string Name { get; set; }

        [DisplayName("身份证号")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{17}[0-9X]$", ErrorMessage = "{0}：为18位数字大写X的组合")]
        public string IDC { get; set; }

        [DisplayName("第一监护人")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,10}$", ErrorMessage = "{0}：2-10个中文字符")]
        public string Name1 { get; set; }

        [DisplayName("第二监护人")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^无$|^[\u4e00-\u9fa5]{2,10}$", ErrorMessage = "{0}：2-10个中文字符｜无")]
        public string Name2 { get; set; }

        [DisplayName("联系电话一")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^1(3[0-9]|4[57]|5[0-35-9]|8[0-9]|7[6-9])\d{8}$", ErrorMessage = "{0}：为11位手机号")]
        public string Mobil1 { get; set; }

        [DisplayName("联系电话二")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^无$|^1(3[0-9]|4[57]|5[0-35-9]|8[0-9]|7[6-9])\d{8}$", ErrorMessage = "{0}：为11位手机号｜无")]
        public string Mobil2 { get; set; }

        [DisplayName("户籍地址")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [StringLength(50, ErrorMessage = "{0}：长度不能超过50")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,10}省[\u4e00-\u9fa5]{2,10}市[\u4e00-\u9fa5]{2,10}(市|区|县)[#-a-zA-Z0-9\u4e00-\u9fa5]{10,20}$|^[\u4e00-\u9fa5]{2,10}市[\u4e00-\u9fa5]{2,10}(市|区|县)[#-a-zA-Z0-9\u4e00-\u9fa5]{10,20}$|^[\u4e00-\u9fa5]{2,10}(市|区|县)[#-a-zA-Z0-9\u4e00-\u9fa5]{10,20}$|^[\u4e00-\u9fa5]{2,10}镇[#-a-zA-Z0-9\u4e00-\u9fa5]{5,20}$", ErrorMessage = "{0}：X省X市X(市区县)X、X市X(市区县)X、姜堰区X、姜堰区X镇X")]
        public string Birth { get; set; }

        [DisplayName("家庭地址")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [StringLength(50, ErrorMessage = "{0}：长度不能超过50")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,10}(市|区|县)[#-a-zA-Z0-9\u4e00-\u9fa5]{10,20}$|^[\u4e00-\u9fa5]{2,10}镇[#-a-zA-Z0-9\u4e00-\u9fa5]{5,20}$", ErrorMessage = "{0}：姜堰区X、X镇X")]
        public string Home { get; set; }

        public bool Checked { get; set; }
        public bool Fixed { get; set; }
    }

    public class BllStudIn : BllEntity<Stud>
    {
        public string ID { get; set; }
        public string IDS { get; set; }

        [DisplayName("年级编号")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{18}$", ErrorMessage = "{0}：18位数字；")]
        public string GradeIDS { get; set; }

        [DisplayName("班级")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{20}$", ErrorMessage = "{0}：20位数字；")]
        public string BanIDS { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{2,10}$", ErrorMessage = "{0}：2-10个汉字；")]
        public string Name { get; set; }

        [DisplayName("身份证号")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^$|^\d{17}[0-9X]$", ErrorMessage = "{0}：为18位数字与X的组合；")]
        public string IDC { get; set; }

        [DisplayName("原班")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "{0}：为4位数字；")]
        public string RegNo { get; set; }

        [DisplayName("学生来源")]
        [Required(ErrorMessage = "{0}：不得为空")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "{0}：用10位数字设置；")]
        public string ComeIDS { get; set; }

        [DisplayName("是否择校")]
        [Required(ErrorMessage = "{0}：不得为空")]
        public bool SchChoose { get; set; }

        public string StepIDS { get; set; }
        public string AccIDS { get; set; }

        public override void ToAdd(ModelStateDictionary model)
        {
            try
            {
                if (!model.IsValid) throw new Exception("表示层：数据验证无法通过！");

                //查询学生信息
                var grade = ViSchGrade.GetEntity<ViSchGrade>(a => a.IDS == this.GradeIDS);
                //学生库记录编号
                var studs = DataCRUD<Stud>.Entitys(a => a.StepIDS == grade.StepIDS);
                var studs_max = studs.Any() ? studs.Max(a => a.IDS) : grade.StepIDS + "0000";
                var studs_max_prev = grade.StepIDS;
                var studs_max_order = int.Parse(studs_max.Substring(studs_max.Length - 4, 4)) + 1;

                //一、学生库添加
                ID = Guid.NewGuid().ToString("N");
                IDS = studs_max_prev + studs_max_order.ToString("D4");
                StepIDS = grade.StepIDS;
                base.ToAdd(model);

                //二、年度学生库添加
                var grades = DataCRUD<StudGrade>.Entitys(a => a.GradeIDS == grade.IDS);
                var grades_max = grades.Any() ? grades.Max(a => a.IDS) : grade.IDS + "0000";
                var grades_max_prev = grade.IDS;
                var grades_max_order = int.Parse(grades_max.Substring(grades_max.Length - 4, 4)) + 1;
                BllStudGrade gradestud = new BllStudGrade()
                {
                    ID = Guid.NewGuid().ToString("N"),
                    IDS = grades_max_prev + grades_max_order.ToString("D4"),
                    GradeIDS = grade.IDS,
                    StudIDS = this.IDS,
                    BanIDS = this.BanIDS,
                    OldBan = this.RegNo,
                    Choose = this.SchChoose,
                    ComeIDS = this.ComeIDS,
                    Fixed = false,
                    InSch = true,
                    OutIDS = null,
                };
                gradestud.ToAdd();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }

    public class BllStudFill : BllEntity<Stud>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Mobil2 { get; set; }
        public string Birth { get; set; }
        public string Home { get; set; }
    }

    public class BllStudReg : BllEntity<Stud>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string IDC { get; set; }
        public string Name { get; set; }
        public string StepIDS { get; set; }
        public string FromSch { get; set; }
        public string Mobil1 { get; set; }
        public string Mobil2 { get; set; }
        public string Memo { get; set; }
        public string AccIDS { get; set; }
        public string RegUID { get; set; }
    }


}