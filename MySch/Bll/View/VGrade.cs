using MySch.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class VGrade
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string PartStepIDS { get; set; }
        public string PartIDS { get; set; }
        public string TreeName { get; set; }
        public string Name { get; set; }
        public string YearName { get; set; }
        public bool Graduated { get; set; }
        public bool IsCurrent { get; set; }
        public bool CanRecruit { get; set; }
        public string AccIDS { get; set; }

        public static override IEnumerable<VGrade> GetEntitys(Expression<Func<VGrade, bool>> where)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    var entitys = (from g in db.TGrades
                                   join ps in db.TPartSteps on g.PartStepIDS equals ps.IDS
                                   join p in db.TParts on ps.PartIDS equals p.IDS
                                   join s in db.TSteps on ps.StepIDS equals s.IDS
                                   join y in db.TYears on g.YearIDS equals y.IDS
                                   join e in db.TEdus on g.EduIDS equals e.IDS
                                   select new VGrade
                                   {
                                       ID = g.ID,
                                       IDS = g.IDS,
                                       PartStepIDS = g.PartStepIDS,
                                       PartIDS = p.IDS,
                                       TreeName = y.Name + "级 - " + e.Name,
                                       Name = p.Name + " - " + s.Name + " - " + e.Name,
                                       YearName = y.Name,
                                       Graduated = s.Graduated,
                                       IsCurrent = y.IsCurrent,
                                       CanRecruit = s.CanRecruit,
                                       AccIDS = ps.AccIDS,
                                   })
                                   .Where(where)
                                   .OrderBy(a => a.IDS)
                                   .ToList();
                    return entitys;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}