using MySch.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class VStep
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string PartIDS { get; set; }
        public string Name { get; set; }
        public string PartName { get; set; }
        public string StepName { get; set; }
        public bool Graduated { get; set; }
        public bool CanRecruit { get; set; }
        public string AccIDS { get; set; }

        public static IEnumerable<VStep> GetEntitys(Expression<Func<VStep, bool>> where)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    var entitys = (from s in db.TSteps
                                   join p in db.TParts on s.PartIDS equals p.IDS
                                   select new VStep
                                   {
                                       ID = s.ID,
                                       IDS = s.IDS,
                                       PartIDS = s.PartIDS,
                                       Name = p.Name + " - " + s.Name,
                                       PartName = p.Name,
                                       StepName = s.Name,
                                       Graduated = s.Graduated,
                                       CanRecruit = s.CanRecruit,
                                       AccIDS = s.AccIDS,
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

        public static VStep GetEntity(Expression<Func<VStep, bool>> where)
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

        public static object GetDataGridPages(Expression<Func<VStep, bool>> where, int pageIndex, int pageSize)
        {
            try
            {
                int skip = (pageIndex - 1) * pageSize;

                var entitys = GetEntitys(where);
                var takes = entitys.Skip(skip).Take(pageSize);

                return EasyUI<VStep>.DataGrids(takes, entitys.Count());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}