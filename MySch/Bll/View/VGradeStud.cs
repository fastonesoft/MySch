﻿using MySch.Bll.Func;
using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class VGradeStud
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string CID { get; set; }
        public string PartIDS { get; set; }
        public string StepIDS { get; set; }
        public string GradeIDS { get; set; }
        public string BanIDS { get; set; }
        public string BanName { get; set; }
        public string BanLongName { get; set; }
        public string StudIDS { get; set; }
        public string StudName { get; set; }
        public string StudSex { get; set; }
        public string ComeName { get; set; }
        public string OutName { get; set; }
        public bool Checked { get; set; }
        public bool Fixed { get; set; }
        public bool InSch { get; set; }
        public bool IsCurrent { get; set; }

        public static IEnumerable<VGradeStud> GetEntitys(Expression<Func<VGradeStud, bool>> where)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    var entitys = (from gs in db.StudGrades
                                   join b in db.TBans on gs.BanIDS equals b.IDS
                                   join g in db.TGrades on gs.GradeIDS equals g.IDS
                                   join s in db.TSteps on g.StepIDS equals s.IDS
                                   join p in db.TParts on s.PartIDS equals p.IDS
                                   join y in db.TYears on g.YearIDS equals y.IDS
                                   join e in db.AEdus on g.EduIDS equals e.IDS
                                   join st in db.Students on gs.StudIDS equals st.IDS
                                   join c in db.SComes on gs.ComeIDS equals c.IDS into gs_cs
                                   from gs_c in gs_cs.DefaultIfEmpty()
                                   join o in db.SOuts on gs.OutIDS equals o.IDS into gs_os
                                   from gs_o in gs_os.DefaultIfEmpty()
                                   select new VGradeStud
                                   {
                                       ID = gs.ID,
                                       IDS = gs.IDS,
                                       CID = st.CID,
                                       PartIDS = p.IDS,
                                       StepIDS = s.IDS,
                                       GradeIDS = gs.GradeIDS,
                                       BanIDS = gs.BanIDS,
                                       BanName = e.Name + "（" + b.Num + "）班",
                                       BanLongName = s.Name + " - " + e.Name + "（" + b.Num + "）班",
                                       StudIDS = gs.StudIDS,
                                       StudName = st.Name,
                                       StudSex = st.CID.Substring(16, 1),
                                       ComeName = gs_c.Name,
                                       OutName = gs_o.Name,
                                       Checked = st.Checked,
                                       Fixed = st.Fixed,
                                       InSch = gs.InSch,
                                       IsCurrent = y.IsCurrent,
                                   })
                                   .Where(where)
                                   .OrderBy(a => a.BanIDS)
                                   .ThenBy(a => a.IDS)
                                   .ToList();
                    //性别转换
                    foreach (var entity in entitys)
                    {

                        entity.StudSex = entity.StudSex == null ? null : int.Parse(entity.StudSex) % 2 == 0 ? "女" : "男";
                    }
                    return entitys;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static VGradeStud GetEntity(Expression<Func<VGradeStud, bool>> where)
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

        public static object GetDataGridPages(Expression<Func<VGradeStud, bool>> where, int pageIndex, int pageSize)
        {
            try
            {
                int skip = (pageIndex - 1) * pageSize;

                var entitys = GetEntitys(where);
                var takes = entitys.Skip(skip).Take(pageSize);

                return EasyUI<VGradeStud>.DataGrids(takes, entitys.Count());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}