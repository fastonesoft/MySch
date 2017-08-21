using MySch.Bll.View;
using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Bll.Entity
{
    public class BllGradeBacks : BllEntity<StudGrade>
    {
        public string ID { get; set; }
        public string IDS { get; set; }

        //主要修改下面几个属性
        public bool InSch { get; set; }
        public string OutIDS { get; set; }
        public Nullable<System.DateTime> OutTime { get; set; }

        //全部回校（临时不在校的学生）
        public static string Backs(IEnumerable<VStudOut> entitys)
        {
            try
            {
                int count = 0;
                foreach (var entity in entitys)
                {
                    if (entity.OutName == "临时")
                    {
                        var back = new BllGradeBacks
                        {
                            ID = entity.ID,
                            IDS = entity.IDS,
                            InSch = true,
                            OutIDS = null,
                            OutTime = null,
                        };
                        back.ToUpdate();
                        count++;
                    }
                }
                return string.Format("选中 {0} 人，实际回校 {1} 人。", entitys.Count(), count);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}