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
    public class VStudOut
    {
        public string CID { get; set; }
        public string PartIDS { get; set; }
        public string GradeIDS { get; set; }
        public string BanIDS { get; set; }
        public string StudName { get; set; }
        public string StudSex { get; set; }
        public string OutName { get; set; }
        public bool InSch { get; set; }

        public static IEnumerable<VStudOut> GetEntitys(Expression<Func<VStudOut, bool>> where)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    var entitys = (from gs in db.TGradeStuds
                                   join c in db.TComes on gs.ComeIDS equals c.IDS into gs_cs
                                   from gs_c in gs_cs.DefaultIfEmpty()
                                   join o in db.TOuts on gs.OutIDS equals o.IDS into gs_os
                                   from gs_o in gs_os.DefaultIfEmpty()
                                   join b in db.TBans on gs.BanIDS equals b.IDS
                                   join g in db.TGrades on gs.GradeIDS equals g.IDS
                                   join ps in db.TPartSteps on g.PartStepIDS equals ps.IDS
                                   join p in db.TParts on ps.PartIDS equals p.IDS
                                   join s in db.TSteps on ps.StepIDS equals s.IDS
                                   join y in db.TYears on g.YearIDS equals y.IDS
                                   join e in db.TEdus on g.EduIDS equals e.IDS
                                   join st in db.TStudents on gs.StudIDS equals st.IDS
                                   select new VStudOut
                                   {
                                       CID = st.CID,
                                       PartIDS = ps.PartIDS,
                                       GradeIDS = gs.GradeIDS,
                                       BanIDS = gs.BanIDS,
                                       StudName = st.Name,
                                       StudSex = st.CID.Substring(16, 1),
                                       OutName = gs_o.Name,
                                       InSch = gs.InSch,
                                   })
                                   .Where(where)
                                   .OrderBy(a => a.BanIDS)
                                   .ToList();
                    return entitys;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static VStudOut GetEntity(Expression<Func<VStudOut, bool>> where)
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

        public static object GetDataGrids(string ids, string memo)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    if (memo == "Part")
                    {
                        //查询显示
                        var part_entitys = VStudOut.GetEntitys(a => a.PartIDS == ids && a.InSch == false);
                        return EasyUI<VStudOut>.DataGrids(part_entitys, part_entitys.Count());
                    }
                    else
                    {
                        if (memo == "Grade")
                        {
                            var grade = db.TGrades.Single(a => a.IDS == ids);
                            var entitys = from s in db.TStudents
                                          where s.PartStepIDS == grade.PartStepIDS && !(from g in db.TGradeStuds
                                                                                        where g.GradeIDS == ids
                                                                                        select g.StudIDS).Contains(s.IDS)
                                          select s;
                            //补缺：把导入数据中以往不正常删除的学生资料，加以恢复
                            //保持，年度学生与学生库的一致
                            var grades = db.TGradeStuds.Where(a => a.GradeIDS == ids);
                            var grades_max = grades.Any() ? grades.Max(a => a.IDS) : ids + "0000";
                            var grades_max_prev = ids;
                            var grades_max_order = int.Parse(grades_max.Substring(grades_max.Length - 4, 4));
                            foreach (var entity in entitys)
                            {
                                grades_max_order++;
                                BllGradeStud gstud = new BllGradeStud();
                                gstud.ID = Guid.NewGuid().ToString("N");
                                gstud.IDS = grades_max_prev + grades_max_order.ToString("D4");
                                gstud.GradeIDS = ids;
                                gstud.StudIDS = entity.IDS;
                                gstud.BanIDS = ids + "01";
                                gstud.OldBan = "0101";
                                gstud.Choose = false;
                                gstud.ComeIDS = "3212840201";
                                gstud.Fixed = false;
                                gstud.InSch = false;
                                gstud.OutIDS = "3212840299";
                                //提交
                                gstud.ToAdd();
                            }
                            //查询显示
                            var grade_entitys = VStudOut.GetEntitys(a => a.GradeIDS == ids && a.InSch == false);
                            return EasyUI<VStudOut>.DataGrids(grade_entitys, grade_entitys.Count());
                        }
                        else
                        {
                            //班级直接查询，因为：先年级，后班级
                            var ban_entitys = VStudOut.GetEntitys(a => a.BanIDS == ids && a.InSch == false);
                            return EasyUI<VStudOut>.DataGrids(ban_entitys, ban_entitys.Count());
                        }
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