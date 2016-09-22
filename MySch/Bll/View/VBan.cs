using MySch.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class VBan:VBase<VBan>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Num { get; set; }
        public string PartStepIDS { get; set; }
        public string GradeIDS { get; set; }
        public string Name { get; set; }
        public string TreeName { get; set; }
        public string MasterName { get; set; }
        public string GroupName { get; set; }
        public bool Graduated { get; set; }
        public string AccIDS { get; set; }

        public static override IEnumerable<VBan> GetEntitys(Expression<Func<VBan, bool>> where)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    var entitys = (from b in db.TBans
                                   join g in db.TGrades on b.GradeIDS equals g.IDS
                                   join ps in db.TPartSteps on g.PartStepIDS equals ps.IDS
                                   join p in db.TParts on ps.PartIDS equals p.IDS
                                   join s in db.TSteps on ps.StepIDS equals s.IDS
                                   join y in db.TYears on g.YearIDS equals y.IDS
                                   join e in db.TEdus on g.EduIDS equals e.IDS
                                   join m in db.TAccs on b.MasterIDS equals m.IDS
                                   join gp in db.TAccs on b.GroupIDS equals gp.IDS
                                   select new VBan
                                   {
                                       ID = b.ID,
                                       IDS = b.IDS,
                                       Num = b.Num,
                                       PartStepIDS = g.PartStepIDS,
                                       GradeIDS = b.GradeIDS,
                                       Name = p.Name + " - " + s.Name + "级 - " + e.Name + "（" + b.Num + ")班",
                                       TreeName = e.Name + "（" + b.Num + ")班",
                                       MasterName = m.Name,
                                       GroupName = gp.Name,
                                       Graduated = s.Graduated,
                                       AccIDS = b.AccIDS,
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