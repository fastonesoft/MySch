using MySch.Core;
using MySch.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class ViPage
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public bool Bootup { get; set; }
        public bool Fixed { get; set; }
        public string ParentID { get; set; }

        public static IEnumerable<ViPage> GetEntitys(Expression<Func<ViPage, bool>> where)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    int total = db.TTerms.Count();

                    var entitys = (from p in db.AdminPages
                                   select new ViPage
                                   {
                                       ID = p.ID,
                                       IDS = p.IDS,
                                       Name = p.Name,
                                       Bootup = p.Bootup,
                                       Fixed = p.Fixed,
                                       ParentID = p.ParentID,
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

        public static ViPage GetEntity(Expression<Func<ViPage, bool>> where)
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

        public static object GetDataGridPages(Expression<Func<ViPage, bool>> where, int pageIndex, int pageSize)
        {
            try
            {
                int skip = (pageIndex - 1) * pageSize;

                var entitys = GetEntitys(where);
                var takes = entitys.Skip(skip).Take(pageSize);

                return EasyUI<ViPage>.DataGrids(takes, entitys.Count());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}