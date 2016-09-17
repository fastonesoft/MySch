using MySch.Bll.Entity;
using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class QGradeStudOut
    {
        public static object GetDataGrid(string ids, string memo)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    if (memo == "Grade")
                    {
                        //查：年度学生
                        var grades = db.TGradeStuds.Where(a => a.GradeIDS == ids);
                        //获取当前年度学生记录编号
                        var grades_max = grades.Max(a => a.IDS);
                        var grades_max_prev = grades_max.Substring(0, grades_max.Length - 4);
                        var grades_max_order = int.Parse(grades_max.Substring(grades_max.Length - 4, 4));
                        //获取当前年度记录
                        var grade = DataCRUD<TGrade>.Entity(a => a.IDS == ids);
                        //查：学生库
                        var students = db.TStudents.Where(a => a.PartStepIDS == grade.PartStepIDS);

                        //检测是否有缺少的学生
                        foreach (var s in students)
                        {
                            //检测当前年度是否存在该学生
                            var g = grades.Select(a => a.StudIDS == s.IDS);
                            //重复检测
                            if (g.Count() > 1)
                            {
                                throw new Exception(string.Format("身份证号为{0}学生存在多重记录！", s.CID));
                            }
                            //不存在则添加为
                            if (g.Count() == 0)
                            {
                                BllGradeStud gstud = new BllGradeStud();
                                gstud.ID = Guid.NewGuid().ToString("N");
                                gstud.IDS = grades_max_prev + grades_max_order++.ToString().PadLeft(4, '0');
                                gstud.GradeIDS = ids;
                                //提交添加
                                gstud.ToAdd();
                            }
                        }
                        //查询显示
                        return QllGradeStud.GetDataGridEntitys<QllGradeStud>(a => a.GradeIDS == ids);
                    }
                    else
                    {
                        //班级直接查询，因为：先年级，后班级
                        return QllGradeStud.GetDataGridEntitys<QllGradeStud>(a => a.BanIDS == ids);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}