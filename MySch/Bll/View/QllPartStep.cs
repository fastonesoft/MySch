using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class QllPartStep
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string PartIDS { get; set; }
        public string StepIDS { get; set; }
        public string AccIDS { get; set; }

        public string Name { get; set; }
        public string PartName { get; set; }
        public string StepName { get; set; }

        //多表连接查询
        public static object GetDataGridPages(Expression<Func<QllPartStep, bool>> where, int pageIndex, int pageSize)
        {
            try
            {
                int skip = (pageIndex - 1) * pageSize;

                //读取：分页实体对象
                using (BaseContext db = new BaseContext())
                {
                    int total = db.TPartSteps.Count();

                    var entitys = (from ps in db.TPartSteps
                                     join p in db.TParts
                                     on ps.PartIDS equals p.IDS
                                     join s in db.TSteps
                                     on ps.StepIDS equals s.IDS
                                     select new QllPartStep
                                     {
                                         ID = ps.ID,
                                         IDS = ps.IDS,
                                         PartIDS = ps.PartIDS,
                                         StepIDS = ps.StepIDS,
                                         AccIDS = ps.AccIDS,
                                         Name = p.Name + " - " + s.Name.ToString() + "级",
                                         PartName = p.Name,
                                         StepName = s.Name.ToString(),
                                     })
                                     .Where(where)
                                     .OrderBy(a => a.IDS)
                                     .Skip(skip)
                                     .Take(pageSize)
                                     .ToList();

                    //输出：转换成DataGrid的数据
                    return EasyUI<QllPartStep>.DataGrids(entitys, total);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static IEnumerable<QllPartStep> GetEntitys(Expression<Func<QllPartStep, bool>> where)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    var entitys = (from ps in db.TPartSteps
                                   join p in db.TParts
                                   on ps.PartIDS equals p.IDS
                                   join s in db.TSteps
                                   on ps.StepIDS equals s.IDS
                                   select new QllPartStep
                                   {
                                       ID = ps.ID,
                                       IDS = ps.IDS,
                                       PartIDS = ps.PartIDS,
                                       StepIDS = ps.StepIDS,
                                       AccIDS = ps.AccIDS,
                                       Name = p.Name + " - " + s.Name.ToString() + "级",
                                       PartName = p.Name,
                                       StepName = s.Name.ToString(),
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

        public static QllPartStep GetEntity(Expression<Func<QllPartStep, bool>> where)
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