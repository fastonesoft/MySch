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
        public string PartIDS { get; set; }
        public string StepIDS { get; set; }
        public string TreeName { get; set; }
        public string Name { get; set; }
        public string YearName { get; set; }
        public bool Graduated { get; set; }
        public bool IsCurrent { get; set; }
        public bool CanRecruit { get; set; }
        public string AccIDS { get; set; }

        public static IEnumerable<VGrade> GetEntitys(Expression<Func<VGrade, bool>> where)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    var entitys = (from g in db.TGrades
                                   join s in db.TSteps on g.StepIDS equals s.IDS
                                   join p in db.TParts on s.PartIDS equals p.IDS
                                   join y in db.TYears on g.YearIDS equals y.IDS
                                   join e in db.TEdus on g.EduIDS equals e.IDS
                                   select new VGrade
                                   {
                                       ID = g.ID,
                                       IDS = g.IDS,
                                       PartIDS = p.IDS,
                                       StepIDS = s.IDS,
                                       TreeName = s.Name + " - " + e.Name,
                                       Name = p.Name + " - " + s.Name + " - " + e.Name,
                                       YearName = y.Name,
                                       Graduated = s.Graduated,
                                       IsCurrent = y.IsCurrent,
                                       CanRecruit = s.CanRecruit,
                                       AccIDS = g.AccIDS,
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

        public static VGrade GetEntity(Expression<Func<VGrade, bool>> where)
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

        public static object GetDataGridPages(Expression<Func<VGrade, bool>> where, int pageIndex, int pageSize)
        {
            try
            {
                int skip = (pageIndex - 1) * pageSize;

                var entitys = GetEntitys(where);
                var takes = entitys.Skip(skip).Take(pageSize);

                return EasyUI<VGrade>.DataGrids(takes, entitys.Count());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}