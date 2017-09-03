using MySch.Dal;
using MySch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Bll.View
{
    public class VBanTree
    {
        public string ID { get; set; }
        public string IDS { get; set; }
        public string Name { get; set; }
        public string Parent { get; set; }

        public static IEnumerable<VBanTree> PartTree(IEnumerable<VBan> bans)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    var entitys = from p in db.TParts.ToList()
                                  join b in bans on p.IDS equals b.PartIDS
                                  select new VBanTree
                                  {
                                      ID = p.ID,
                                      IDS = p.IDS,
                                      Name = p.Name,
                                      Parent = null,
                                  };

                    return entitys.Distinct(a => a.IDS);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static IEnumerable<VBanTree> GradeTree(IEnumerable<VBan> bans, string partIDS)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    var entitys = from g in db.TGrades.ToList()
                                  join b in bans on g.IDS equals b.GradeIDS
                                  join e in db.TEdus.ToList() on g.EduIDS equals e.IDS
                                  join s in db.TSteps.ToList() on g.StepIDS equals s.IDS
                                  where s.PartIDS == partIDS
                                  select new VBanTree
                                  {
                                      ID = g.ID,
                                      IDS = g.IDS,
                                      Name = s.Name + " - " + e.Name,
                                      Parent = s.PartIDS,
                                  };

                    return entitys.Distinct(a => a.IDS);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static IEnumerable<VBanTree> BanTree(IEnumerable<VBan> bans, string gradeIDS)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    var entitys = from b in bans
                                  where b.GradeIDS == gradeIDS
                                  select new VBanTree
                                  {
                                      ID = b.ID,
                                      IDS = b.IDS,
                                      Name = b.TreeName,
                                      Parent = b.GradeIDS,
                                  };

                    return entitys.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}