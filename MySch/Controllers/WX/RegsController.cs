using MySch.Bll.Custom;
using MySch.Bll.WX.Model;
using MySch.Bll.WX.ViewModel;
using MySch.Core;
using MySch.Mvvm.School.Stud;
using MySch.Mvvm.School.Stud.Action;
using MySch.Mvvm.Wall;
using MySch.Mvvm.Wall.Action;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MySch.Controllers.WX
{
    public class RegsController : Controller
    {
        public ActionResult Index(WX_OAuth auth)
        {
            try
            {
                var user = auth.GoneLogin();
                user.codePage = Setting.Url(Request);
                //检测是否绑定学生
                user.BindingStud();
                //缓存
                user.ToSession();

                return View();
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        /// <summary>
        /// 返回js注册用的信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Jssdk()
        {
            try
            {
                //检查中控
                var token = WX_AccessToken.GetAccessToken();
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                //签名算法
                var signature = new WX_Signature(WX_Const.goneAppID, WX_Jsticket.GetJsticket(token), infor.codePage, infor.idc, infor.name, infor.regno, infor.exam, infor.examuid, infor.rexamuid);

                //序列化
                return Json(signature);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult JssdkMs()
        {
            try
            {
                //检查中控
                var token = WX_AccessToken.GetAccessToken();
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();
                //检测权限
                WX_OAuserInfor.CheckExamRoleMs(infor.unionid);

                //签名算法
                var signature = new WX_Signature(WX_Const.goneAppID, WX_Jsticket.GetJsticket(token), infor.codePage, infor.idc, infor.name, infor.regno, infor.exam, infor.examuid, infor.rexamuid);

                //序列化
                return Json(signature);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult JssdkEx()
        {
            try
            {
                //检查中控
                var token = WX_AccessToken.GetAccessToken();
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();
                //检测权限
                WX_OAuserInfor.CheckExamRoleEx(infor.unionid);

                //签名算法
                var signature = new WX_Signature(WX_Const.goneAppID, WX_Jsticket.GetJsticket(token), infor.codePage, infor.idc, infor.name, infor.regno, infor.exam, infor.examuid, infor.rexamuid);

                //序列化
                return Json(signature);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult JssdkGd()
        {
            try
            {
                //检查中控
                var token = WX_AccessToken.GetAccessToken();
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();
                //检测权限
                WX_OAuserInfor.CheckExamRoleGrade(infor.unionid);

                //签名算法
                var signature = new WX_Signature(WX_Const.goneAppID, WX_Jsticket.GetJsticket(token), infor.codePage, infor.idc, infor.name, infor.regno, infor.exam, infor.examuid, infor.rexamuid);

                //序列化
                return Json(signature);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        //注册
        [HttpPost]
        public ActionResult Reg(string idc, string mobil1)
        {
            try
            {
                var idcu = idc.ToUpper();
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                var error = IDC.IDS(idcu);
                //返回身份证错误
                if (error.error) return Json(new ErrorMessage { error = true, message = new WX_KeyValue { key = "regs_reg_idc", value = error.message } });

                var name = ActionStudent.RegStudent(idcu, mobil1, infor.unionid);
                return Json(new ErrorMessage { error = false, message = new WX_KeyValue { key = idcu, value = name } });
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = new WX_KeyValue { value = e.Message } });
            }
        }

        //自行上传
        [HttpPost]
        public ActionResult UploadImageSelf(string mediaID, string uploadType)
        {
            try
            {
                //检测页面、用户
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                var res = WX_UploadImage.SaveImageSelf(mediaID, uploadType, infor.unionid);
                return Json(new ErrorMessage { error = false, message = res });
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        //////////////////////////////////////////////

        //审核页面
        public ActionResult Examine(WX_OAuth auth)
        {
            try
            {
                var user = auth.GoneLogin();
                user.codePage = Setting.Url(Request);
                user.ToSession();

                return View();
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpPost]
        public ActionResult GetImagesType()
        {
            try
            {
                //检测页面、用户
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                //根据身份证获取图片列表
                var res = WX_UploadImage.GetImagesType(infor.unionid);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        //图片分类
        [HttpPost]
        public ActionResult GetImagesTypeByIdc(string idc)
        {
            try
            {
                //检测页面、用户
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                //根据身份证获取图片列表
                var res = WX_UploadImage.GetImagesTypeByIdc(idc);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult GetImagesTypeByScan(string idc)
        {
            try
            {
                //检测页面、用户
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                //根据身份证获取图片列表
                var res = WX_UploadImage.GetImagesTypeByScan(idc);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        //图片删除
        [HttpPost]
        public ActionResult DeleteImage(string url)
        {
            try
            {
                //检测页面、用户
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var user = WX_OAuserInfor.GetFromSession();

                var res = WX_UploadImage.DeleteImage(url);
                return Json(new ErrorMessage { error = false, message = res });
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        //带人上传
        [HttpPost]
        public ActionResult UploadImageOther(string mediaID, string uploadType, string Other)
        {
            try
            {
                //检测页面、用户
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                //代别人上传
                var res = WX_UploadImage.SaveImageForOther(mediaID, uploadType, Other);
                return Json(new ErrorMessage { error = false, message = res });
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        //通过初审
        [HttpPost]
        public ActionResult PassExamine(string ID, bool Choose)
        {
            try
            {
                //检测页面、用户
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                //审核
                return Json(WX_Examine.Examine(ID, Choose, infor.unionid));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        //通过初审人员查询，未通过初审人数统计
        [HttpPost]
        public ActionResult ExamedStuds()
        {
            try
            {
                //检测页面、用户
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                //审核过的学生
                return Json(WX_Examine.ExamedStuds(infor.unionid));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }


        ////////////////////////////////////////////////

        //退回审核  ->  材料复核
        public ActionResult Rexamine(WX_OAuth auth)
        {
            try
            {
                var user = auth.GoneLogin();
                user.codePage = Setting.Url(Request);
                user.ToSession();

                return View();
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpPost]
        public ActionResult GetInforByIdc(string idc)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                var sinfor = StudInfor.StudRexamineByIdc(idc, infor.unionid);
                return Json(sinfor);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult GetInforByScan(string idc)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                var sinfor = StudInfor.StudRexamineByScan(idc, infor.unionid);
                return Json(sinfor);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        //通过复核
        [HttpPost]
        public ActionResult RexamById(string id)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(WX_Examine.Rexamine(id, infor.unionid));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        //退回重审
        [HttpPost]
        public ActionResult RollById(string id)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                WX_Examine.Roll(id);
                return Json(new ErrorMessage { error = false, message = "复核材料退回" });
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult RexamedStuds()
        {
            try
            {
                //检测页面、用户
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                //审核过的学生
                return Json(WX_Examine.RexamedStuds(infor.unionid));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }


        /////////////////////////////
        //手动注册
        public ActionResult AddMana(WX_OAuth auth)
        {
            try
            {
                var user = auth.GoneLogin();
                user.codePage = Setting.Url(Request);
                user.ToSession();

                return View();
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpPost]
        public ActionResult ManaReg(string idc, string mobil1)
        {
            try
            {
                var idcu = idc.ToUpper();
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                var error = IDC.IDS(idcu);
                //返回身份证错误
                if (error.error) return Json(new ErrorMessage { error = true, message = new WX_KeyValue { key = "regs_mana_idc", value = error.message } });

                var name = ActionStudent.RegStudent(idcu, mobil1);
                return Json(new ErrorMessage { error = false, message = new WX_KeyValue { key = idcu, value = name } });
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = new WX_KeyValue { value = e.Message } });
            }
        }


        //外省添加
        public ActionResult AddOut(WX_OAuth auth)
        {
            try
            {
                var user = auth.GoneLogin();
                user.codePage = Setting.Url(Request);
                user.ToSession();

                return View();
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpPost]
        public ActionResult OutReg(string idc, string mobil1, string name, string school)
        {
            try
            {
                var idcu = idc.ToUpper();
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                var error = IDC.IDS(idcu, 2003, 2006);
                //返回身份证错误
                if (error.error) return Json(new ErrorMessage { error = true, message = new WX_KeyValue { key = "regs_out_idc", value = error.message } });

                ActionStudent.RegStudent(idcu, mobil1, name, school);
                return Json(new ErrorMessage { error = false, message = new WX_KeyValue { key = idcu, value = name } });
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = new WX_KeyValue { value = e.Message } });
            }
        }

        //绑定学生
        public ActionResult Scan(WX_OAuth auth)
        {
            try
            {
                var user = auth.GoneLogin();
                user.codePage = Setting.Url(Request);
                user.BindingStud();
                user.ToSession();

                return View();
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpPost]
        public ActionResult BindStudByScan(string idc)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                var name = StudInfor.BindStudByScan(idc, infor.unionid);
                return Json(new ErrorMessage { error = false, message = name });
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult UnBindStud(string id)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                StudInfor.UnBindStud(id);
                return Json(new ErrorMessage { error = false, message = "解除绑定成功" });
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }


        /////////////////////////////
        //房产信息
        public ActionResult House(WX_OAuth auth)
        {
            try
            {
                var user = auth.GoneLogin();
                user.codePage = Setting.Url(Request);
                user.ToSession();

                return View();
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpPost]
        //申请一个
        public ActionResult NotFixed()
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                var res = VmStudEx.NotFixed();
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        //[HttpPost]
        //扫描一下
        public ActionResult UrlByID(string id)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                var res = VmStudEx.UrlByID(id);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        //根据录取编号查学生
        public ActionResult UrlByRegNo(string id)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                var res = VmStudEx.UrlByRegNo(id);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }


        [HttpPost]
        //提交上去
        public ActionResult FixStud(VmStudEx stud)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                var res = VmStudEx.FixStud(stud);
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult NotFixedCount()
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                var res = VmStudEx.NotFixedCount();
                return Json(res);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        public ActionResult Mast(WX_OAuth auth)
        {
            try
            {
                var user = auth.GoneLogin();
                user.codePage = Setting.Url(Request);
                user.ToSession();

                return View();
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        //查询学生姓名
        [HttpPost]
        public ActionResult MaSearchStuds(string id)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(ActionStudGrade.SearchStuds(infor.unionid, id));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        //调动学生
        [HttpPost]
        public ActionResult MaMoveStud(string id)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(ActionStudGrade.MoveStud(infor.unionid, id));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        //显示学生“查询”二维码
        [HttpPost]
        public ActionResult MaMoveCode(string id)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                var data = ActionStudGrade.MoveCode(infor.unionid, id);
                var dataurl = "http://a.jysycz.cn/image/codem?id=" + data.Value + "&title=" + data.Key + "&r=" + DateTime.Now.Ticks.ToString();

                return Json(dataurl);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        //显示学生“确认”二维码
        [HttpPost]
        public ActionResult MaConfirmCode(string id, string id2)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                var data = ActionStudGrade.ConfirmCode(infor.unionid, id, id2);
                var dataurl = "http://a.jysycz.cn/image/coder?id=" + data.Value + "&title=" + data.Key + "&r=" + DateTime.Now.Ticks.ToString();

                return Json(dataurl);
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        //识别学生二维码
        [HttpPost]
        public ActionResult MaMoveScan(VmStudGradeMove data)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                if (data.Command == "query")
                {
                    return Json(ActionStudGrade.MoveScanQuery(infor.unionid, data));
                }
                if (data.Command == "confirm")
                {
                    return Json(ActionStudGrade.MoveScanMove(infor.unionid, data));
                }

                return Json(new ErrorMessage { error = true, message = "传输数据有误" });
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }


        [HttpPost]
        public ActionResult MaMoveBanNum()
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(ActionStudGrade.MoveBanNum(infor.unionid));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }


        //删除要调动的学生
        [HttpPost]
        public ActionResult MaReMoveStud(string id)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(ActionStudGrade.RemoveStud(infor.unionid, id));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        //调动学生列表
        [HttpPost]
        public ActionResult MaMoveStuds()
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(ActionStudGrade.MoveStuds(infor.unionid));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        //调动成功学生列表
        [HttpPost]
        public ActionResult MaMovedStuds()
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(ActionStudGrade.MovedStuds(infor.unionid));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        ////////////调动参数修改
        [HttpPost]
        public ActionResult MaMoveInforSet(bool isabs, bool samesex)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();
                ActionStudGrade.MoveInforSet(infor.unionid, isabs, samesex);
                return Json(new ErrorMessage { error = false, message = "班级调动参数已变更" });
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        /////////////////////
        [HttpPost]
        public ActionResult MaMoveInforGet()
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();
                var baninfor = ActionStudGrade.MoveInforGet(infor.unionid);

                return Json(new ErrorMessage { error = false, message = baninfor });
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult MaMovedGone()
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(ActionStudGrade.MovedGone(infor.unionid));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        //墙跳转页
        public ActionResult Req(string id)
        {
            try
            {
                var url = WX_Url.MenuView(WX_Const.goneAppID, "http://a.jysycz.cn/regs/wall", id);
                return Redirect(url);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        //墙页面
        public ActionResult Wall(WX_OAuth auth)
        {
            try
            {
                var user = auth.GoneLogin();
                user.codePage = Setting.Url(Request);
                user.ToSession();

                //检测二维码
                ActionWall.CheckState(auth.state);
                return View();
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        //查询教师是否注册
        [HttpPost]
        public ActionResult TeachReged()
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(ActionWall.TeachReged(infor.unionid));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult TeachReg(string id)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(ActionWall.TeachReg(infor, id));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult SendedMsg()
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(ActionWall.SendedMsg(infor.unionid));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult SendMsg(string id)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(ActionWall.SendMsg(infor.unionid, id));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult SendMsgNotShow()
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(ActionWall.SendMsgNotShow(5));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult SendMsgShowed()
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(ActionWall.SendMsgShowed());
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult AccPrize(IEnumerable<VqWxAccSend> entitys)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(ActionWall.AccPrize(entitys));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult AccPrized()
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(ActionWall.AccPrized());
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        //班级照片
        public ActionResult Photo(WX_OAuth auth)
        {
            try
            {
                var user = auth.GoneLogin();
                user.codePage = Setting.Url(Request);
                user.ToSession();

                return View();
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpPost]
        public ActionResult BanStudents(string id)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(ActionStudPhoto.BanStudents(id));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult BanPhotoUpload(string mediaID, string studIDS)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                return Json(ActionStudPhoto.BanPhotoUpload(mediaID, studIDS));
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult BanPhotoDelete(string imageID, string studIDS)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                ActionStudPhoto.BanPhotoDelete(imageID, studIDS);
                return Json(new ErrorMessage { error = false, message = "删除成功" });
            }
            catch (Exception e)
            {
                return Json(new ErrorMessage { error = true, message = e.Message });
            }
        }


    }
}