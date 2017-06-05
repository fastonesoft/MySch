using MySch.Dal;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace MySch.Bll.WX.Model
{
    public class WX_UploadImage
    {

        public static string SaveImage(string wxtoken, string mediaid, string path)
        {
            try
            {
                //文件准备
                var fileName = Guid.NewGuid().ToString("N");
                var filePath = HttpContext.Current.Server.MapPath(string.Format(path, fileName, "jpg"));

                //保存
                var web = new WebClient();
                web.DownloadFile(WX_Url.MediaFile(wxtoken, mediaid), filePath);

                return fileName;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string SaveImage(string wxtoken, string mediaid)
        {
            try
            {
                return SaveImage(wxtoken, mediaid, "~/Upload/XueImages/{0}.{1}");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 将临时图片保存
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="mediaID"></param>
        /// <param name="openID"></param>
        public static string SaveImageSelf(string mediaID, string uploadType, string reguid)
        {
            try
            {
                //根据openID读取学生编号
                var db = DataCRUD<Student>.Entity(a => a.RegUID == reguid);
                if (db == null) throw new Exception("未绑定学生，不能上传");

                //中控token
                var wxtoken = WX_AccessToken.GetAccessToken();

                //保存
                var fileName = WX_UploadImage.SaveImage(wxtoken, mediaID);

                //下载成功,记录
                var upload = new WxUploadFile
                {
                    ID = fileName,
                    IDS = db.IDS,
                    FileType = "jpg",
                    UploadType = uploadType,
                    CreateTime = DateTime.Now,
                };
                DataCRUD<WxUploadFile>.Add(upload);

                //返回文件名称
                return fileName;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string SaveImageForOther(string mediaID, string uploadType, string Other)
        {
            try
            {
                var db = DataCRUD<Student>.Entity(a => a.ID == Other);
                if (db == null)throw new Exception("未查询到当前学生信息");

                //中控token
                var wxtoken = WX_AccessToken.GetAccessToken();

                //保存
                var fileName = WX_UploadImage.SaveImage(wxtoken, mediaID);

                //下载成功,记录
                var upload = new WxUploadFile
                {
                    ID = fileName,
                    IDS = db.IDS,
                    FileType = "jpg",
                    UploadType = uploadType,
                    CreateTime = DateTime.Now,
                };
                DataCRUD<WxUploadFile>.Add(upload);

                //提示信息
                return fileName;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static WX_KeyValue GetImagesType(string reguid)
        {
            try
            {
                var entity = DataCRUD<Student>.Entity(a => a.RegUID == reguid);
                if (entity == null)
                {
                    throw new Exception("无法查询绑定的学生");
                }
                else
                {
                    //
                    var res = new WX_KeyValue();
                    res.key = entity.Name;
                    res.value = entity.Examed;

                    var uploads = DataCRUD<WxUploadFile>.Entitys(a => a.IDS == entity.IDS).OrderBy(a => a.CreateTime);
                    foreach (var upload in uploads)
                    {
                        res.Add(upload.ID, upload.UploadType);
                    }
                    return res;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //身份证分类查询图片
        public static WX_KeyValue GetImagesTypeByIdc(string idc)
        {
            try
            {
                var entity = DataCRUD<Student>.Entity(a => a.IDC == idc);
                if (entity == null) throw new Exception("无法识别的扫码信息");
                if (entity.Examed) throw new Exception(string.Format("【{0}】的资料已通过审核", entity.Name));

                var res = new WX_KeyValue();
                res.key = entity.Name;
                res.value = entity.ID;
                var resx = res.Add("choose", entity.SchChoose);

                var uploads = DataCRUD<WxUploadFile>.Entitys(a => a.IDS == entity.IDS).OrderBy(a => a.CreateTime);
                foreach (var upload in uploads)
                {
                    resx.Add(upload.ID, upload.UploadType);
                }
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //能否删除
        public static WxUploadFile CanDelete(string id)
        {
            try
            {
                var entity = DataCRUD<WxUploadFile>.Entity(a => a.ID == id);
                if (entity == null) throw new Exception("没有找到图片对应文件");
                //检测对应学生是否已审核
                var stud = DataCRUD<Student>.Entity(a => a.IDS == entity.IDS);
                if (stud == null) throw new Exception("没有找到对应的学生");
                if (stud.Examed) throw new Exception("已经通过审核，不能删除");
                return entity;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //删除图片
        public static string DeleteImage(string url)
        {
            try
            {
                //根据“=”将参数分隔：http://control/action/?name=XXXXXXXX
                var id = url.Split('=');
                var entity = CanDelete(id[1]);
                DataCRUD<WxUploadFile>.Delete(entity);

                return "图片已删除";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}