using MySch.Bll.Entity;
using MySch.Bll.View;
using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MySch.Mvvm.School.Student.Action
{
    public class ActionStudGrade
    {
        public static string FengBan(string gradeids)
        {
            try
            {
                var grade = BllGrade.GetEntity<BllGrade>(a => a.IDS == gradeids);
                if (grade == null) throw new Exception("年级编号查询出错！");
                if (!grade.CanFeng) throw new Exception("分班已完成，无法再进行相关动作！");

                //参加分班的班级
                var fengs = DataCRUD<TBan>.Entitys(a => a.GradeIDS == gradeids && !a.NotFeng).OrderBy(a => a.IDS);
                //准备班级S列表
                var sb = new StringBuilder();
                var banlist0 = new List<string>();
                foreach (var feng in fengs)
                {
                    sb.Append(feng.IDS + ",");
                    banlist0.Add(feng.IDS);
                }
                var strfengs = sb.ToString();

                //准备班级S列表：后段  => 女生段 banlist1
                var banlist2 = new List<string>();
                var banlist1 = new List<string>(banlist0);
                var banlist0count = banlist0.Count();
                for (var i = banlist0count - 1; i >= 0; i--)
                {
                    banlist1.Add(banlist0[i]);
                    banlist2.Add(banlist0[i]);
                }
                //banlist2  男生段
                banlist2.AddRange(banlist0);

                //统计总数
                var stbans = VGradeStud.GetEntitys(a => a.GradeIDS == gradeids && a.InSch && strfengs.Contains(a.BanIDS))
                    .OrderByDescending(a => a.Score)
                    .ThenBy(a => a.ID);

                //分班：女生表
                var stbans1 = stbans.Where(a => a.StudSex == "女");
                //分班：男生表
                var stbans2 = stbans.Where(a => a.StudSex == "男");

                //女生分班
                var count1 = 0;
                var ban1count = banlist1.Count();
                foreach (var student in stbans1)
                {
                    var i = count1 % ban1count;
                    var newban = new VmStudGradeBan
                    {
                        ID = student.ID,
                        IDS = student.IDS,
                        BanIDS = banlist1[i],
                    };
                    count1++;
                    newban.ToUpdate();
                }

                //男生分班
                var count2 = 0;
                var ban2count = banlist2.Count();
                foreach (var student in stbans2)
                {
                    var i = count2 % ban2count;
                    var newban = new VmStudGradeBan
                    {
                        ID = student.ID,
                        IDS = student.IDS,
                        BanIDS = banlist2[i],
                    };
                    count2++;
                    newban.ToUpdate();
                }

                //设置分班结束标志
                grade.CanFeng = false;
                grade.ToUpdate();

                //统计分班信息
                return string.Format("在班总人数为：{0}，实际参加分班人数：{1}", stbans.Count(), stbans1.Count() + stbans2.Count());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}