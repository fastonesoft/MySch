using MySch.Core;
using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class ViStud 
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public string IDC { get; set; }
        public string StepIDS { get; set; }
        public string Mobil1 { get; set; }
        public string Mobil2 { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Home { get; set; }
        public string Birth { get; set; }
        public bool Fixed { get; set; }

        public string StepName{get;set;}

        public static IEnumerable<ViStud> GetEntitys(Expression<Func<ViStud, bool>> where)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    var entitys = (from st in db.Studs
                                   join s in db.TSteps on st.StepIDS equals s.IDS
                                   select new ViStud
                                   {
                                       ID = st.ID,
                                       IDS = st.IDS,
                                       Name = st.Name,
                                       IDC = st.IDC,
                                       StepIDS = st.StepIDS,
                                       Mobil1 = st.Mobil1,
                                       Mobil2 = st.Mobil2,
                                       Name1 = st.Name1,
                                       Name2 = st.Name2,
                                       Home = st.Home,
                                       Birth = st.Birth,
                                       Fixed = st.Fixed,
                                       StepName = s.Name,
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

        public static ViStud GetEntity(Expression<Func<ViStud, bool>> where)
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

        public static object GetDataGridPages(Expression<Func<ViStud, bool>> where, int pageIndex, int pageSize)
        {
            try
            {
                int skip = (pageIndex - 1) * pageSize;

                var entitys = GetEntitys(where);
                var takes = entitys.Skip(skip).Take(pageSize);

                return EasyUI<ViStud>.DataGrids(takes, entitys.Count());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}