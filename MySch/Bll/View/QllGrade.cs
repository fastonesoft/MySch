using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class QllGrade : BllBase<TGrade>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string EduIDS { get; set; }
        public string PartStepIDS { get; set; }
        public string YearIDS { get; set; }
        public string AccIDS { get; set; }

        public string EduName { get; set; }
        public string PartName { get; set; }
        public int StepName { get; set; }
        public int YearName { get; set; }

        //多表连接查询
        public static object GetDataGridQPages(Expression<Func<QllGrade, bool>> where, int pageIndex, int pageSize)
        {
            try
            {
                int skip = (pageIndex - 1) * pageSize;

                //读取：分页实体对象
                using (BaseContext db = new BaseContext())
                {
                    var grades = (from g in db.TGrades
                                  join e in db.TEdus
                                  on g.EduIDS equals e.IDS
                                  join y in db.TYears
                                  on g.YearIDS equals y.IDS
                                  join ps in db.TPartSteps
                                  on g.PartStepIDS equals ps.IDS
                                  join p in db.TParts
                                  on ps.PartIDS equals p.IDS
                                  join s in db.TSteps
                                  on ps.StepIDS equals s.IDS
                                  select new QllGrade
                                  {
                                      ID = g.ID,
                                      IDS = g.IDS,
                                      PartStepIDS = g.PartStepIDS,
                                      EduIDS = g.EduIDS,
                                      YearIDS = g.YearIDS,
                                      AccIDS = g.AccIDS,
                                      EduName = e.Name,
                                      PartName = p.Name,
                                      StepName = s.Name,
                                      YearName = y.Name
                                  })
                                  .Where(where)
                                  .OrderBy(a => a.IDS)
                                  .Skip(skip)
                                  .Take(pageSize)
                                  .ToList();

                    //输出：转换成DataGrid的数据
                    return EasyUI<QllGrade>.DataGrids(grades);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}