using MySch.Bll;
using MySch.Bll.Func;
using MySch.Bll.Retu;
using MySch.Bll.WX;
using MySch.Bll.WX.Form;
using MySch.Bll.WX.Model;
using MySch.Bll.Xue;
using MySch.Helper;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
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
                return Json(new BllError { error = true, message = e.Message });
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
                WX_OAuserInfor.CheckExamRole(infor.unionid);

                //签名算法
                var signature = new WX_Signature(WX_Const.goneAppID, WX_Jsticket.GetJsticket(token), infor.codePage, infor.idc, infor.name, infor.regno, infor.exam, infor.examuid, infor.rexamuid);

                //序列化
                return Json(signature);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
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
                if (error.error) return Json(new BllError { error = true, message = new WX_KeyValue { key = "regs_reg_idc", value = error.message } });

                var name = AutoXue.RegStudent(idcu, mobil1, infor.unionid);
                return Json(new BllError { error = false, message = new WX_KeyValue { key = idcu, value = name } });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = new WX_KeyValue { value = e.Message } });
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
                return Json(new BllError { error = false, message = res });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
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
                return Json(new BllError { error = true, message = e.Message });
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
                return Json(new BllError { error = true, message = e.Message });
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
                return Json(new BllError { error = true, message = e.Message });
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
                return Json(new BllError { error = false, message = res });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
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
                return Json(new BllError { error = false, message = res });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        //审核动作
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
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        //审核人数查询
        [HttpPost]
        public ActionResult PassStuds()
        {
            try
            {
                //检测页面、用户
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                //审核过的学生
                return Json(WX_Examine.PassStuds(infor.unionid));
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
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

                var sinfor = StudInfor.StudRexamineByIdc(idc);
                return Json(sinfor);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult GetInforByScan(string idc)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                var sinfor = StudInfor.StudRexamineByScan(idc);
                return Json(sinfor);
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult RexamById(string id)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                WX_Examine.Rexamine(id, infor.unionid);
                return Json(new BllError { error = false, message = "材料复核成功" });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult RollById(string id)
        {
            try
            {
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                WX_Examine.Rexamine(id, infor.unionid);
                return Json(new BllError { error = false, message = "复核材料退回" });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult NotPassStuds()
        {
            try
            {
                //检测页面、用户
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                //审核过的学生
                return Json(WX_Examine.NotPassStuds());
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
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
                if (error.error) return Json(new BllError { error = true, message = new WX_KeyValue { key = "regs_mana_idc", value = error.message } });

                var name = AutoXue.RegStudent(idcu, mobil1);
                return Json(new BllError { error = false, message = new WX_KeyValue { key = idcu, value = name } });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = new WX_KeyValue { value = e.Message } });
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
                if (error.error) return Json(new BllError { error = true, message = new WX_KeyValue { key = "regs_out_idc", value = error.message } });

                AutoXue.RegStudent(idcu, mobil1, name, school);
                return Json(new BllError { error = false, message = new WX_KeyValue { key = idcu, value = name } });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = new WX_KeyValue { value = e.Message } });
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
                return Json(new BllError { error = false, message = name });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}