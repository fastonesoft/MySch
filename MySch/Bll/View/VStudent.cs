using MySch.Bll.Func;
using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class VStudent 
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

        public static IEnumerable<VStudent> GetEntitys(Expression<Func<VStudent, bool>> where)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    var entitys = (from st in db.Students
                                   join s in db.TSteps on st.StepIDS equals s.IDS
                                   select new VStudent
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

        public static VStudent GetEntity(Expression<Func<VStudent, bool>> where)
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

        public static object GetDataGridPages(Expression<Func<VStudent, bool>> where, int pageIndex, int pageSize)
        {
            try
            {
                int skip = (pageIndex - 1) * pageSize;

                var entitys = GetEntitys(where);
                var takes = entitys.Skip(skip).Take(pageSize);

                return EasyUI<VStudent>.DataGrids(takes, entitys.Count());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}