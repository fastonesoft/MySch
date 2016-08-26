using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class QllPartStep : BllBase<TPartStep>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string PartIDS { get; set; }
        public string StepIDS { get; set; }
        public string AccIDS { get; set; }

        public string PartName { get; set; }
        public int StepName { get; set; }

        //多表连接查询
        public static object GetDataGridQPages(Expression<Func<QllPartStep, bool>> where, int pageIndex, int pageSize)
        {
            try
            {
                int skip = (pageIndex - 1) * pageSize;

                //读取：分页实体对象
                using (BaseContext db = new BaseContext())
                {
                    var partsteps = (from ps in db.TPartSteps
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
                                         PartName = p.Name,
                                         StepName = s.Name
                                     })
                                     .Where(where)
                                     .OrderBy(a => a.IDS)
                                     .Skip(skip)
                                     .Take(pageSize)
                                     .ToList();

                    //输出：转换成DataGrid的数据
                    return EasyUI<QllPartStep>.DataGrids(partsteps);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}