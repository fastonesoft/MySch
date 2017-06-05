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
                var signature = new WX_Signature(WX_Const.goneAppID, WX_Jsticket.GetJsticket(token), infor.codePage, infor.idc, infor.name, infor.exam);

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
        public ActionResult Reg(string idc, string mobil1, string mobil2)
        {
            try
            {
                var idcu = idc.ToUpper();
                var oaken = WX_AccessTokenOauth.GetSessionToken();
                var infor = WX_OAuserInfor.GetFromSession();

                var error = IDC.IDS(idcu);
                //返回身份证错误
                if (error.error) return Json(new BllError { error = true, message = new WX_KeyValue { key = "regs_reg_idc", value = error.message } });

                var name = AutoXue.RegStudent(idcu, mobil1, mobil2, infor.unionid);
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
                var user = WX_OAuserInfor.GetFromSession();

                //审核
                WX_Examine.Examine(ID, Choose, user.unionid);
                return Json(new BllError { error = false, message = "审核提交成功" });
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }

        ////////////////////////////////////////////////

        //退回审核
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
                var infor = StudInfor.StudRexamine(idc);
                return Json(infor);
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
                WX_Examine.Rexamine(id);


                return Content("");
            }
            catch (Exception e)
            {
                return Json(new BllError { error = true, message = e.Message });
            }
        }
    }
}