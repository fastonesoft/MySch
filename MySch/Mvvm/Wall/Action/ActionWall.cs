using MySch.Bll.WX.Model;
using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Mvvm.Wall.Action
{
    public class ActionWall
    {
        //检测二维码参数是否与当前活动吻合
        public static void CheckState(string state)
        {
            try
            {
                VmWxAction.GetEntity<VmWxAction>(a => a.ID == state && a.IsCurrent, "二维码失效！");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static VmWxAcc TeachReged(string unionid)
        {
            try
            {
                return VmWxAcc.GetEntityOrDefault<VmWxAcc>(a => a.IDS == unionid);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static VmWxAcc TeachReg(WX_OAuserInfor infor, string name)
        {
            try
            {
                //检测是否注册
                VmWxAcc.GetEntityExists<VmWxAcc>(a => a.IDS == infor.unionid, name + "，你已注册，无需重复动作！");
                var filt = VmWxAccFilt.GetEntity<VmWxAccFilt>(a => a.Name == name, name + "，教师列表中没有你的名字！");

                var teach = new VmWxAccTable
                {
                    ID = Guid.NewGuid().ToString("N"),
                    IDS = infor.unionid,
                    Name = name,
                    Mobil = filt.IDS,
                    Mobils = filt.Mobils,
                    headimgurl = infor.headimgurl,
                    nickname = infor.nickname,
                    openid = infor.openid,
                };
                teach.ToAdd();

                //返回教师相关信息
                return VmWxAcc.GetEntityOrDefault<VmWxAcc>(a => a.ID == teach.ID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static IEnumerable<VqWxAccSend> SendedMsg(string unionid)
        {
            try
            {
                return VqWxAccSend.GetEntitys<VqWxAccSend>(a => a.WxAccIDS == unionid);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static VqWxAccSend SendMsg(string unionid, string message)
        {
            try
            {
                var msg = message.Replace(" ", "");
                VqWxAccSend.GetEntityExists<VqWxAccSend>(a => a.SendMsg == msg && a.WxAccIDS == unionid, "不要提交相同的信息！");
                VmWxAcc.GetEntity<VmWxAcc>(a => a.IDS == unionid, "你好象还没有注册！");

                var action = VmWxAction.GetEntity<VmWxAction>(a => a.IsCurrent, "还没有标识当前活动！");
                var send = new VmWxAccSend
                {
                    ID = Guid.NewGuid().ToString("N"),
                    IDS = Guid.NewGuid().ToString("N"),
                    CreateTime = DateTime.Now,
                    MsgType = "text",
                    SendMsg = msg,
                    WxAccIDS = unionid,
                    WxActionIDS = action.IDS,
                };
                send.ToAdd();

                return VqWxAccSend.GetEntityOrDefault<VqWxAccSend>(a => a.ID == send.ID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static IEnumerable<VqWxAccSend> SendMsgNotShow(int take)
        {
            try
            {
                var notshows = VqWxAccSend.GetEntitys<VqWxAccSend>(a => a.Showed == false).OrderBy(a => a.CreateTime).Take(take);
                foreach (var notshow in notshows)
                {
                    var setshow = new VmWxAccSendToShow
                    {
                        ID = notshow.ID,
                        IDS = notshow.IDS,
                        Showed = true,
                    };
                    setshow.ToUpdate();
                }
                return notshows;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static IEnumerable<VqWxAccSend> SendMsgShowed()
        {
            try
            {
                //开奖列表
                var prizes = VmWxAccPrize.GetEntitys<VmWxAccPrize>(a => true);
                var prizestr = string.Empty;
                foreach (var prize in prizes)
                {
                    prizestr += (prize.WxAccIDS + ",");
                }
                //去除中奖的
                return VqWxAccSend.GetEntitys<VqWxAccSend>(a => a.Showed && !prizestr.Contains(a.WxAccIDS));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}