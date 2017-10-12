using MySch.Bll.Entity;
using MySch.Bll.View;
using MySch.Bll.WX.Model;
using MySch.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MySch.Mvvm.School.Student.Action
{
    public class ActionStudPhoto
    {
        public static object BanStudents(string id)
        {
            try
            {

                var ban = BllStudGrade.GetEntity<BllStudGrade>(a => a.ID == id, "无法识别的扫码信息！");
                var photos = VqBanPhotos.GetEntitys<VqBanPhotos>(a => a.BanIDS == ban.BanIDS && a.InSch && a.UploadType == "photo")
                    .OrderByDescending(a => a.StudSex)
                    .ThenByDescending(a => a.Score)
                    .ThenBy(a => a.StudGradeID);

                //返回数据
                var res = new List<VqBanPhotos>();
                var studidstr = string.Empty;
                foreach (var photo in photos)
                {
                    res.Add(photo);
                    studidstr += (photo.IDS + ",");
                }

                //准备没有图片的学生名单
                var noimages = VGradeStud.GetEntitys(a => a.BanIDS == ban.BanIDS && !studidstr.Contains(a.StudIDS) && a.InSch);
                foreach (var noimage in noimages)
                {
                    var noimagestud = new VqBanPhotos
                    {
                        ID = "",
                        IDS = noimage.StudIDS,
                        Num = noimage.BanNum,
                        Name = noimage.StudName,
                        StudSex = noimage.StudSex,
                        Score = noimage.Score,
                        StudGradeID = noimage.ID,
                    };
                    //添加没图片学生进列表
                    res.Add(noimagestud);
                }

                //读取当前记录所在位置
                var index = 0;
                foreach (var photo in res)
                {
                    if (photo.StudGradeID == id) break;
                    index++;
                }
                return new WX_Key { key = new WX_Key { key = index, value = ban.BanIDS }, value = res };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static object BanPhotoUpload(string mediaID, string studIDS)
        {
            try
            {
                //中控token
                var wxtoken = WX_AccessToken.GetAccessToken();

                //下载
                var fileName = Guid.NewGuid().ToString("N");
                var filePath = HttpContext.Current.Server.MapPath(string.Format("~/Upload/XueImages/{0}.jpg", fileName));
                WX_Url.MediaFile(wxtoken, mediaID, filePath);

                //记录
                var upload = new BllImageUpload
                {
                    ID = fileName,
                    IDS = studIDS,
                    FileType = "jpg",
                    UploadType = "photo",
                    CreateTime = DateTime.Now,
                };
                upload.ToAdd();
                //
                return fileName;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void BanPhotoDelete(string imageID, string studIDS)
        {
            try
            {
                var del = new BllImageDelete
                {
                    ID = imageID,
                    IDS = studIDS,
                };
                del.ToDelete();

                var filename = HttpContext.Current.Server.MapPath(string.Format("/Upload/XueImages/{0}.jpg", imageID));
                if (File.Exists(filename)) File.Delete(filename);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}