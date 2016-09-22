﻿using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class VGradeStuds
    {
        public string CID { get; set; }
        public string PartIDS { get; set; }
        public string GradeIDS { get; set; }
        public string BanIDS { get; set; }
        public string BanName { get; set; }
        public string StudName { get; set; }
        public string StudSex { get; set; }
        public string ComeName { get; set; }
        public string OutName { get; set; }
        public bool Checked { get; set; }
        public bool InSch { get; set; }

        public static IEnumerable<VGradeStuds> GetEntitys(Expression<Func<VGradeStuds, bool>> where)
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
                                   select new VGradeStuds
                                   {
                                       CID = st.CID,
                                       PartIDS = p.IDS,
                                       GradeIDS = gs.GradeIDS,
                                       BanIDS = gs.BanIDS,
                                       BanName = e.Name + "（" + b.Num + "）班",
                                       StudName = st.Name,
                                       StudSex = st.CID.Substring(16, 1),
                                       ComeName = gs_c.Name,
                                       OutName = gs_o.Name,
                                       Checked = st.Checked,
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

        public static VGradeStuds GetEntity(Expression<Func<VGradeStuds, bool>> where)
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

        public static object GetDataGridPages(Expression<Func<VGradeStuds, bool>> where, int pageIndex, int pageSize)
        {
            try
            {
                int skip = (pageIndex - 1) * pageSize;

                var entitys = GetEntitys(where);
                var takes = entitys.Skip(skip).Take(pageSize);

                return EasyUI<VGradeStuds>.DataGrids(takes, entitys.Count());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}