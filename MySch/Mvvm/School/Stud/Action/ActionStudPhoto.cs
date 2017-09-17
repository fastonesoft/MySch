using MySch.Bll.WX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Mvvm.School.Stud.Action
{
    public class ActionStudPhoto
    {
        public static object BanStudents(string id)
        {
            try
            {
                var ban = VqBanPhotos.GetEntity<VqBanPhotos>(a => a.StudGradeID == id && a.UploadType == "photo", "无法识别的扫码信息！");
                var photos = VqBanPhotos.GetEntitys<VqBanPhotos>(a => a.BanIDS == ban.BanIDS && a.InSch && a.UploadType == "photo")
                    .OrderByDescending(a => a.StudSex)
                    .ThenByDescending(a => a.Score)
                    .ThenBy(a => a.StudGradeID);

                //读取当前记录所在位置
                var index = 0;
                foreach (var photo in photos)
                {
                    index++;
                    if (photo.ID == id) break;
                }
                //返回数据
                return new WX_Key { key = new WX_Key { key = index, value = ban.BanIDS }, value = photos };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}