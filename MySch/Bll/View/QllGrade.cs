using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class QllGrade
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string EduIDS { get; set; }
        public string PartStepIDS { get; set; }
        public string YearIDS { get; set; }
        public string AccIDS { get; set; }

        public string Name { get; set; }
        public string EduName { get; set; }
        public string PartName { get; set; }
        public string StepName { get; set; }
        public string YearName { get; set; }

        //多表连接查询
        public static object GetDataGridPages(Expression<Func<QllGrade, bool>> where, int pageIndex, int pageSize)
        {
            try
            {
                int skip = (pageIndex - 1) * pageSize;

                //读取：分页实体对象
                using (BaseContext db = new BaseContext())
                {
                    int total = db.TGrades.Count();

                    var entitys = (from g in db.TGrades
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
                                      Name = p.Name + " - " + s.Name + "级 - " + e.Name,
                                      EduName = e.Name,
                                      PartName = p.Name,
                                      StepName = s.Name.ToString(),
                                      YearName = y.Name.ToString()
                                  })
                                  .Where(where)
                                  .OrderBy(a => a.IDS)
                                  .Skip(skip)
                                  .Take(pageSize)
                                  .ToList();

                    //输出：转换成DataGrid的数据
                    return EasyUI<QllGrade>.DataGrids(entitys, total);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static IEnumerable<QllGrade> GetEntitys(Expression<Func<QllGrade, bool>> where)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    var entitys = (from g in db.TGrades
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
                                      Name = p.Name + " - " + s.Name + "级 - " + e.Name,
                                      EduName = e.Name,
                                      PartName = p.Name,
                                      StepName = s.Name.ToString(),
                                      YearName = y.Name.ToString()
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

        public static QllGrade GetEntity(Expression<Func<QllGrade, bool>> where)
        {
            try
            {
                return GetEntitys(where).Single();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}