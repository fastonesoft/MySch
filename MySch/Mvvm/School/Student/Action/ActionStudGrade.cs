using MySch.Bll.Entity;
using MySch.Bll.Func;
using MySch.Bll.View;
using MySch.Bll.WX.Model;
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
                return VGradeStud.GetEntitys(a => a.InSch && a.GradeIDS == ban.GradeIDS && !a.Fixed && string.IsNullOrEmpty(a.GroupID) && a.StudName.Contains(name) && !movestr.Contains(a.ID));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //调动学生
        public static object MoveStud(string owner, string id)
        {
            try
            {
                //是否可以调到
                var stud = VGradeStud.GetEntity(a => a.InSch && !a.Fixed && string.IsNullOrEmpty(a.GroupID) && a.ID == id);
                if (stud == null) throw new Exception("异常数据");
                //是否已在调动
                var count = BllStudentMove.Count(a => a.ID == id);
                if (count != 0) throw new Exception(stud.StudName + "，已经在调动列表当中！");

                //查询一下自已的班
                var ban = VBan.GetEntity(a => a.MasterIDS == owner);
                if (ban == null) throw new Exception("你还没有带班");
                if (ban.NotFeng) throw new Exception("你班未参加分班，用不着查找");

                //统计调动人数
                var fixedcount = BllGradeStud.Count(a => a.Fixed && a.BanIDS == ban.IDS);
                var movecount = BllStudentMove.Count(a => a.OwnerAccIDS == owner);
                if (fixedcount + movecount >= ban.ChangeNum) throw new Exception("已达到调动人数上限");


                //如果学生已在本条上
                //直接设置固定，并给出提示信息
                if (stud.BanIDS == ban.IDS)
                {
                    var studfix = BllGradeStud.GetEntity<BllGradeStud>(a => a.ID == stud.ID);
                    if (studfix == null) throw new Exception("异常数据");
                    //固定
                    studfix.GroupID = Guid.NewGuid().ToString("N");
                    studfix.ToUpdate();
                    throw new Exception(stud.StudName + "，原本就在本班，已做了固定标志！");
                }

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

        //调动“查询”所需的二维码
        public static VmKeyValue MoveCode(string owner, string id)
        {
            try
            {
                //查找调动学生信息
                var move = BllStudentMove.GetEntity<BllStudentMove>(a => a.ID == id);
                if (move == null) throw new Exception("没有找到学生的调动信息");
                if (move.OwnerAccIDS != owner) throw new Exception("调动中的学生，不是你发起的");

                //查询学生姓名
                var stud = VGradeStud.GetEntity(a => a.ID == id);

                //准备查询二维码数据
                var code = new VmStudGradeMove
                {
                    ID = move.ID,
                    IDS = move.IDS,
                    BanIDS = move.BanIDS,
                    OwnerAccIDS = move.OwnerAccIDS,
                    Command = "query",
                };

                //
                var res = new VmKeyValue
                {
                    Key = stud.StudName,
                    Value = code.ToUrlString(),
                };
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 调动“确认”所需的二维码
        /// </summary>
        /// <param name="owner">被调班主任</param>
        /// <param name="id">备用交换学生</param>
        /// <param name="id2">被调学生</param>
        /// <param name="owner2">调动申请班主任</param>
        /// <returns></returns>
        public static VmKeyValue ConfirmCode(string owner, string id, string id2)
        {
            try
            {
                //查找：学生信息
                var movestudent = VGradeStud.GetEntity(a => a.ID == id2);
                if (movestudent == null) throw new Exception("没有找到编号对应学生信息！");

                //查找：学生调动信息
                var move = BllStudentMove.GetEntity<BllStudentMove>(a => a.ID == id2);
                if (move == null) throw new Exception(movestudent.StudName + "，还没有调动记录！");

                //检查：调动学生是不是我班的
                var ban = VBan.GetEntity(a => a.MasterIDS == owner);
                if (move.BanIDS != ban.IDS) throw new Exception(movestudent.StudName + "，不是我班的！");


                //准备交换学生信息
                var student = VGradeStud.GetEntity(a => a.ID == id);
                if (student == null) throw new Exception("没有找到编号对应学生信息！");

                //查找交换学生是否在调当中
                var count = BllStudentMove.Count(a => a.ID == id);
                if (count != 0) throw new Exception(student.StudName + "，已在调动列表当中");

                //准备查询二维码数据
                var code = new VmStudGradeMove
                {
                    ID = move.ID,
                    IDS = move.IDS,
                    BanIDS = move.BanIDS,
                    OwnerAccIDS = move.OwnerAccIDS,
                    Command = "confirm",
                    ID2 = student.ID,
                    IDS2 = student.IDS,
                    BanIDS2 = student.BanIDS,
                    OwnerAccIDS2 = owner,
                };

                //
                var res = new VmKeyValue
                {
                    Key = student.StudName,
                    Value = code.ToUrlString(),
                };
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //识别一：查询满足条件的学生
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

                var res = new VmKeyValue { Command = data.Command, Key = data.ID };
                //可以交换的学生列表：在校、在班、没定、没组
                var students = VGradeStud.GetEntitys(a => a.InSch && a.BanIDS == who.IDS && !a.Fixed && string.IsNullOrEmpty(a.GroupID));
                //性别，分差
                if (ban.SameSex)
                {
                    res.Value = !ban.IsAbs ?
                        students.Where(a => a.StudSex == stud.StudSex && a.Score >= stud.Score && a.Score <= stud.Score + ban.Differ).OrderBy(a => a.Score).Take(5) :
                        students.Where(a => a.StudSex == stud.StudSex && a.Score >= stud.Score - ban.Differ && a.Score <= stud.Score + ban.Differ).OrderBy(a => a.Score).Take(5);
                }
                else
                {
                    res.Value = !ban.IsAbs ?
                        students.Where(a => a.StudSex != stud.StudSex && a.Score >= stud.Score && a.Score <= stud.Score + ban.Differ).OrderBy(a => a.Score).Take(5) :
                        students.Where(a => a.StudSex != stud.StudSex && a.Score >= stud.Score - ban.Differ && a.Score <= stud.Score + ban.Differ).OrderBy(a => a.Score).Take(5);
                }
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //识别二：
        public static object MoveScanMove(string owner, VmStudGradeMove data)
        {
            try
            {
                //身份认定
                if (data.OwnerAccIDS != owner) throw new Exception("异常数据");

                //先保存交换学生
                var change = new BllStudentMove
                {
                    ID = data.ID2,
                    IDS = data.IDS2,
                    BanIDS = data.BanIDS2,
                    OwnerAccIDS = data.OwnerAccIDS2,
                };
                change.ToAdd();
                //可以保存，开始调动
                //交换两个学生信息
                var studin = new VmStudGradeBanWanted
                {
                    ID = data.ID,
                    IDS = data.IDS,
                    BanIDS = data.BanIDS2,
                    Fixed = true,
                };
                studin.ToUpdate();
                var studout = new VmStudGradeBan
                {
                    ID = data.ID2,
                    IDS = data.IDS2,
                    BanIDS = data.BanIDS,
                };
                studout.ToUpdate();
                //准备删除信息
                var move = new BllStudentMove
                {
                    ID = data.ID,
                    IDS = data.IDS,
                };
                move.ToDelete();
                change.ToDelete();

                //查询学生信息
                var res = new VmKeyValue { Command = data.Command };
                res.Value = VGradeStud.GetEntity(a => a.ID == data.ID);
                //返回学生编号
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static object MoveBanNum(string owner)
        {
            try
            {
                var ban = VBan.GetEntity(a => a.MasterIDS == owner);
                if (ban == null) throw new Exception("你还没有带班呢");

                //查找你调几个了，
                var key = BllGradeStud.Count(a => a.Fixed && a.BanIDS == ban.IDS);
                var value = BllStudentMove.Count(a => a.OwnerAccIDS == owner);
                return new VmKeyValue
                {
                    Command = ban.ChangeNum.ToString(),
                    Key = key.ToString(),
                    Value = (ban.ChangeNum - key - value).ToString()
                };
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