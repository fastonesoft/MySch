using MySch.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class VBan
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Num { get; set; }
        public string PartIDS { get; set; }
        public string PartStepIDS { get; set; }
        public string GradeIDS { get; set; }
        public string Name { get; set; }
        public string TreeName { get; set; }
        public string MasterName { get; set; }
        public string GroupName { get; set; }
        public bool Graduated { get; set; }
        public string AccIDS { get; set; }

        public static  IEnumerable<VBan> GetEntitys(Expression<Func<VBan, bool>> where)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    var entitys = (from b in db.TBans
                                   join g in db.TGrades on b.GradeIDS equals g.IDS
                                   join s in db.TSteps on g.StepIDS equals s.IDS
                                   join p in db.TParts on s.PartIDS equals p.IDS
                                   join y in db.TYears on g.YearIDS equals y.IDS
                                   join e in db.TEdus on g.EduIDS equals e.IDS
                                   join m in db.TAccs on b.MasterIDS equals m.IDS into b_ms
                                   from b_m in b_ms.DefaultIfEmpty()
                                   join gp in db.TAccs on b.GroupIDS equals gp.IDS into b_gps
                                   from b_gp in b_gps.DefaultIfEmpty()
                                   select new VBan
                                   {
                                       ID = b.ID,
                                       IDS = b.IDS,
                                       Num = b.Num,
                                       PartIDS = p.IDS,
                                       GradeIDS = b.GradeIDS,
                                       Name = p.Name + " - " + s.Name + "级 - " + e.Name + "（" + b.Num + "）班",
                                       TreeName = e.Name + "（" + b.Num + "）班",
                                       MasterName = b_m.Name,
                                       GroupName = b_gp.Name,
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
        public static VBan GetEntity(Expression<Func<VBan, bool>> where)
        {
            try
            {
                var entity = GetEntitys(where);
                return entity.Count() == 1 ? entity.Single() : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static object GetDataGridPages(Expression<Func<VBan, bool>> where, int pageIndex, int pageSize)
        {
            try
            {
                int skip = (pageIndex - 1) * pageSize;

                var entitys = GetEntitys(where);
                var takes = entitys.Skip(skip).Take(pageSize);

                return EasyUI<VBan>.DataGrids(takes, entitys.Count());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}