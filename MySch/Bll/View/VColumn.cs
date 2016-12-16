using MySch.Bll.Func;
using MySch.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class VColumn
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public bool Fixed { get; set; }
        public string PageIDS { get; set; }
        public string PageName { get; set; }

        public static IEnumerable<VColumn> GetEntitys(Expression<Func<VColumn, bool>> where)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    var entitys = (from c in db.TColumns
                                   join p in db.TPages on c.PageIDS equals p.IDS
                                   select new VColumn
                                   {
                                       ID = c.ID,
                                       IDS = c.IDS,
                                       Name = c.Name,
                                       Fixed = c.Fixed,
                                       PageIDS = c.PageIDS,
                                       PageName = p.Name,
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

        public static VColumn GetEntity(Expression<Func<VColumn, bool>> where)
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

        public static object GetDataGridPages(Expression<Func<VColumn, bool>> where, int pageIndex, int pageSize)
        {
            try
            {
                int skip = (pageIndex - 1) * pageSize;

                var entitys = GetEntitys(where);
                var takes = entitys.Skip(skip).Take(pageSize);

                return EasyUI<VColumn>.DataGrids(takes, entitys.Count());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}