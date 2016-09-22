using MySch.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class VTerm : VBase<VTerm>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public bool IsCurrent { get; set; }
        public string AccIDS { get; set; }

        public static override IEnumerable<VTerm> GetEntitys(Expression<Func<VTerm, bool>> where)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    int total = db.TTerms.Count();

                    var entitys = (from t in db.TTerms
                                   join s in db.TSemesters on t.SemesterIDS equals s.IDS
                                   join y in db.TYears on t.YearIDS equals y.IDS
                                   select new VTerm
                                   {
                                       ID = t.ID,
                                       IDS = t.IDS,
                                       Name = y.Name + "年度 - " + s.Name,
                                       IsCurrent = t.IsCurrent,
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

    }
}