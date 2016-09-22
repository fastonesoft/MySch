using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.Model
{
    public class QTerm
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public bool IsCurrent { get; set; }
        public string AccIDS { get; set; }

        public string YearName { get; set; }

        //多表连接查询
        //var query = from a in data.A
        //            join b in data.B
        //            on a.bid equals b.id into ab
        //            from b in ab.DefaultIfEmpty()
        //            join c in data.C
        //            on b.cid equals c.id into bc
        //            from c in bc.DefaultIfEmpty()
        //            join d in data.D
        //            on c.did equals d.id into cd
        //            from d in cd.DefaultIfEmpty()
        //            join e in data.E
        //            on d.eid equals e.id into ce
        //            from e in ce.DefaultIfEmpty()
        //            select new Signature
        //            {
        //                Id = a.Id,
        //                Name = b == null ? "" : b.Name,
        //                Pro = c == null ? "" : c.Pro,
        //                IpAdd = d == null ? "" : d.IpAdd
        //            }; 
        public static object GetDataGridPages(Expression<Func<QTerm, bool>> where, int pageIndex, int pageSize)
        {
            try
            {
                int skip = (pageIndex - 1) * pageSize;
                //读取：分页实体对象
                using (BaseContext db = new BaseContext())
                {
                    int total = db.TTerms.Count();

                    var entitys = db.TTerms.Join(db.TYears, t => t.YearIDS, y => y.IDS, (t, y) => new QTerm
                    {
                        ID = t.ID,
                        IDS = t.IDS,
                        Name = t.Name,
                        IsCurrent = t.IsCurrent,
                        AccIDS = t.AccIDS,
                        YearName = y.Name.ToString()
                    })
                    .Where(where)
                    .OrderBy(a => a.IDS)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();

                    //输出：转换成DataGrid的数据
                    return EasyUI<QTerm>.DataGrids(entitys, total);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}