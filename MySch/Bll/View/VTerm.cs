using MySch.Core;
using MySch.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class VTerm
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public bool IsCurrent { get; set; }
        public string YearIDS { get; set; }
        public string AccIDS { get; set; }

        public static IEnumerable<VTerm> GetEntitys(Expression<Func<VTerm, bool>> where)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    int total = db.TTerms.Count();

                    var entitys = (from t in db.TTerms
                                   join s in db.TTermTypes on t.TermTypeIDS equals s.IDS
                                   join y in db.TYears on t.YearIDS equals y.IDS
                                   select new VTerm
                                   {
                                       ID = t.ID,
                                       IDS = t.IDS,
                                       Name = y.Name + "年度 - " + s.Name,
                                       IsCurrent = t.IsCurrent,
                                       YearIDS = t.YearIDS,
                                       AccIDS = t.AccIDS,
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

        public static VTerm GetEntity(Expression<Func<VTerm, bool>> where)
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

        public static object GetDataGridPages(Expression<Func<VTerm, bool>> where, int pageIndex, int pageSize)
        {
            try
            {
                int skip = (pageIndex - 1) * pageSize;

                var entitys = GetEntitys(where);
                var takes = entitys.Skip(skip).Take(pageSize);

                return EasyUI<VTerm>.DataGrids(takes, entitys.Count());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}