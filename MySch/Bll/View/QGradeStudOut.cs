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
                        var grade = db.TGrades.Single(a => a.IDS == ids);
                        var entitys = from s in db.TStudents
                                      where s.PartStepIDS == grade.PartStepIDS && !(from g in db.TGradeStuds
                                                                                    where g.GradeIDS == ids
                                                                                    select g.StudIDS).Contains(s.IDS)
                                      select s;

                        var grades_max = db.TGradeStuds.Max(a => a.IDS);
                        var grades_max_prev = grades_max.Substring(0, grades_max.Length - 4);
                        var grades_max_order = int.Parse(grades_max.Substring(grades_max.Length - 4, 4));
                        foreach (var entity in entitys)
                        {
                            grades_max_order++;
                            BllGradeStud gstud = new BllGradeStud();
                            gstud.ID = Guid.NewGuid().ToString("N");
                            gstud.IDS = grades_max_prev + grades_max_order.ToString().PadLeft(4, '0');
                            gstud.GradeIDS = ids;
                            gstud.StudIDS = entity.IDS;
                            gstud.BanIDS = ids + "01";
                            gstud.OldBan = "0101";
                            gstud.Choose = false;
                            gstud.ComeIDS = "3212840201";
                            gstud.Fixed = false;
                            gstud.InSch = false;
                            gstud.OutIDS = "3212840206";
                            //提交
                            gstud.ToAdd();
                        }
                        //查询显示
                        return QllGradeStud.GetDataGridEntitys<QllGradeStud>(a => a.GradeIDS == ids && a.InSch == false);
                    }
                    else
                    {
                        //班级直接查询，因为：先年级，后班级
                        return QllGradeStud.GetDataGridEntitys<QllGradeStud>(a => a.BanIDS == ids && a.InSch == false);
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