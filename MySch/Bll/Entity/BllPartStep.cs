using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
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
        [RegularExpression(@"^\d{10}$", ErrorMessage = "{0}：用10位数字设置；")]
        public string PartIDS { get; set; }

        [DisplayName("分级编号")]
        [Required(ErrorMessage = "{0}不得为空；")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "{0}：用12位数字设置；")]
        public string StepIDS { get; set; }

        public string Name { get; set; }

        public string AccIDS { get; set; }

        public static IEnumerable<BllPartStep> GetPartSteps(Expression<Func<BllPartStep, bool>> where)
        {
            using (BaseContext db = new BaseContext())
            {
                var partsteps = (from ps in db.TPartSteps
                                 join p in db.TParts
                                 on ps.PartIDS equals p.IDS
                                 join s in db.TSteps
                                 on ps.StepIDS equals s.IDS
                                 select new BllPartStep
                                 {
                                     ID = ps.ID,
                                     IDS = ps.IDS,
                                     PartIDS = ps.PartIDS,
                                     StepIDS = ps.StepIDS,
                                     AccIDS = ps.AccIDS,
                                     Name = p.Name + " - " + s.Name + "级"
                                 })
                                 .Where(where)
                                 .OrderBy(a => a.IDS)
                                 .ToList();
                return partsteps;
            }
        }
    }
}