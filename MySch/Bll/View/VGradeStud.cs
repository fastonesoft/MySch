using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.View
{
    public class VGradeStud :VBase<VGradeStud>
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string GradeIDS { get; set; }
        public string BanIDS { get; set; }
        public string StudIDS { get; set; }
        public string StudCode { get; set; }
        public bool Choose { get; set; }
        public string ComeIDS { get; set; }
        public bool Fixed { get; set; }
        public bool InSch { get; set; }
        public string PartIDS { get; set; }
        public string BanName { get; set; }
        public string DataGridName { get; set; }
        public bool Graduated { get; set; }
        public string StudName { get; set; }
        public string StudSex { get; set; }
        public string CID { get; set; }
        public string ComeName { get; set; }
        public string OutName { get; set; }
        public bool Checked { get; set; }

        public static override IEnumerable<VGradeStud> GetEntitys(Expression<Func<VGradeStud, bool>> where)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    var entitys = (from gs in db.TGradeStuds
                                   join b in db.TBans on gs.BanIDS equals b.IDS
                                   join g in db.TGrades on gs.GradeIDS equals g.IDS
                                   join ps in db.TPartSteps on g.PartStepIDS equals ps.IDS
                                   join p in db.TParts on ps.PartIDS equals p.IDS
                                   join s in db.TSteps on ps.StepIDS equals s.IDS
                                   join y in db.TYears on g.YearIDS equals y.IDS
                                   join e in db.TEdus on g.EduIDS equals e.IDS
                                   select new VGradeStud
                                   {
                                       ID = gs.ID,
                                       IDS = gs.IDS,
                                       GradeIDS = gs.GradeIDS,
                                       BanIDS = gs.BanIDS,
                                       StudIDS = gs.StudIDS,

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
    }
}