using MySch.Bll.Func;
using MySch.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class VDatum
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }

        public static IEnumerable<VDatum> GetEntitys(Expression<Func<VDatum, bool>> where)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    int total = db.TTerms.Count();

                    var entitys = (from d in db.ADatums
                                   select new VDatum
                                   {
                                       ID = d.ID,
                                       IDS = d.IDS,
                                       Name = d.Name,
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

        public static VDatum GetEntity(Expression<Func<VDatum, bool>> where)
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

        public static object GetDataGridPages(Expression<Func<VDatum, bool>> where, int pageIndex, int pageSize)
        {
            try
            {
                int skip = (pageIndex - 1) * pageSize;

                var entitys = GetEntitys(where);
                var takes = entitys.Skip(skip).Take(pageSize);

                return EasyUI<VDatum>.DataGrids(takes, entitys.Count());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}