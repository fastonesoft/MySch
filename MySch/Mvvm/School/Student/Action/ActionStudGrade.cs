﻿using MySch.Bll.Entity;
using MySch.Bll.Func;
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
        //分班
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
                var stbans = VGradeStud.GetEntitys(a => a.GradeIDS == gradeids && a.InSch && strfengs.Contains(a.BanIDS));

                //分班：女生表
                var stbans1 = stbans.Where(a => a.StudSex == "女")
                    .OrderByDescending(a => a.Score)
                    .ThenBy(a => a.ID);
                //分班：男生表
                var stbans2 = stbans.Where(a => a.StudSex == "男")
                    .OrderByDescending(a => a.Score)
                    .ThenBy(a => a.ID);

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

        //查询调动学生
        public static object SearchStuds(string owner, string name)
        {
            try
            {
                //查询年级
                var ban = VBan.GetEntity(a => a.MasterIDS == owner);
                if (ban == null) throw new Exception("你还没有带班");
                if (ban.NotFeng) throw new Exception("你班未参加分班，用不着查找");

                //构建调动学生列表
                var moves = BllStudentMove.GetEntitys<BllStudentMove>(a => true);
                var sb = new StringBuilder();
                foreach (var move in moves)
                {
                    sb.Append(move.ID + ",");
                }
                var movestr = sb.ToString();
                //
                return VGradeStud.GetEntitys(a => a.GradeIDS == ban.GradeIDS && !a.Fixed && string.IsNullOrEmpty(a.GroupID) && a.StudName.Contains(name) && !movestr.Contains(a.ID));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static object MoveStud(string owner, string id)
        {
            try
            {
                //是否可以调到
                var stud = VGradeStud.GetEntity(a => !a.Fixed && string.IsNullOrEmpty(a.GroupID) && a.ID == id);
                if (stud == null) throw new Exception("异常数据");
                //是否已在调动
                var count = BllStudentMove.Count(a => a.ID == id);
                if (count != 0) throw new Exception(stud.StudName + "，已经在调动列表当中！");

                //添加进调动列表
                var save = new BllStudentMove
                {
                    ID = stud.ID,
                    IDS = stud.IDS,
                    BanIDS = stud.BanIDS,
                    OwnerAccIDS = owner,
                };
                save.ToAdd();
                //返回
                return stud;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static object MoveCode(string owner, string id)
        {
            try
            {
                //查找调动学生信息
                var move = BllStudentMove.GetEntity<BllStudentMove>(a => a.OwnerAccIDS == owner && a.ID == id);
                if (move == null) throw new Exception("没有找到学生的调动信息");

                //准备查询二维码数据
                var code = new VmStudGradeMove
                {
                    ID = move.ID,
                    IDS = move.IDS,
                    BanIDS = move.BanIDS,
                    OwnerAccIDS = owner,
                    Command = "query",
                };

                return code.ToUrlString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //查询满足条件的学生
        public static object MoveScanQuery(string owner, VmStudGradeMove data)
        {
            try
            {
                //条件是什么：
                //  是我的条件下的，你班的学生
                //一、我的条件
                var ban = VBan.GetEntity(a => a.MasterIDS == owner);
                if (ban == null) throw new Exception("你有带班吗？");
                if (ban.NotFeng) throw new Exception("你有学生跟人换吗？");
                //调动的“我班”学生信息
                var stud = VGradeStud.GetEntity(a => a.ID == data.ID && a.IDS == data.IDS && a.BanIDS == ban.IDS);
                if (stud == null) throw new Exception("不是你班学生，或者信息无法识别！");
                //哪个班的要跟我调
                var who = VBan.GetEntity(a => a.MasterIDS == data.OwnerAccIDS);
                if (who == null) throw new Exception("找不到调动学生的班级");

                //分差，性别
                if (ban.SameSex)
                {
                    return !ban.IsAbs ?
                        VGradeStud.GetEntitys(a => a.BanIDS == who.IDS && a.StudSex == stud.StudSex && a.Score >= stud.Score && a.Score <= stud.Score + ban.Differ).Take(5) :
                         VGradeStud.GetEntitys(a => a.BanIDS == who.IDS && a.StudSex == stud.StudSex && a.Score >= stud.Score - ban.Differ && a.Score <= stud.Score + ban.Differ).Take(5);
                }
                else
                {
                    return !ban.IsAbs ?
                        VGradeStud.GetEntitys(a => a.BanIDS == who.IDS && a.StudSex != stud.StudSex && a.Score >= stud.Score && a.Score <= stud.Score + ban.Differ).Take(5) :
                         VGradeStud.GetEntitys(a => a.BanIDS == who.IDS && a.StudSex != stud.StudSex && a.Score >= stud.Score - ban.Differ && a.Score <= stud.Score + ban.Differ).Take(5);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static object MoveScanMove()
        {
            try
            {
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static object RemoveStud(string owner, string id)
        {
            try
            {
                var remove = BllStudentMove.GetEntity<BllStudentMove>(a => a.ID == id && a.OwnerAccIDS == owner);
                if (remove == null) throw new Exception("没有找到你要删除的调动学生");

                remove.ToDelete();
                return id;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        //调动中的学生列表
        public static object MoveStuds(string owner)
        {
            try
            {
                //构建调动学生列表
                var moves = BllStudentMove.GetEntitys<BllStudentMove>(a => a.OwnerAccIDS == owner);
                var sb = new StringBuilder();
                foreach (var move in moves)
                {
                    sb.Append(move.ID + ",");
                }
                var movestr = sb.ToString();

                //查询调动列表当中学生信息
                return VGradeStud.GetEntitys(a => movestr.Contains(a.ID));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //调动成功的学生列表
        public static object MovedStuds(string owner)
        {
            try
            {
                //查询我的班级
                var ban = VBan.GetEntity(a => a.MasterIDS == owner);
                if (ban == null) throw new Exception("你还没有带班");

                //查询我带的班，调动学生名单
                return VGradeStud.GetEntitys(a => a.BanIDS == ban.IDS && a.Fixed);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}