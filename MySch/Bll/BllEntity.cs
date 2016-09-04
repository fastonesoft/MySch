using MySch.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MySch.Bll
{
    /// <summary>
    /// 表示层 数据表 操作基类：功能：查询、增、改、删
    /// 一、提供单数据记录的增、改、删操作
    /// 二、提供静态的单个、多重数据记录查询操作
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    public class BllEntity<Entity> : BllBase<Entity> where Entity : class
    {
        //数据输出到EasyUI的DataGrid
        public object ToDataGrid()
        {
            List<object> objes = new List<object>();
            objes.Add(this);
            return new { total = 1, rows = objes };
        }

        //通过反射的方式 将 表示数据 -> 实体对象
        public void ToAdd(ModelStateDictionary model)
        {
            try
            {
                if (model.IsValid)
                {
                    BllApi<Entity>.Add(this);
                }
                else
                {
                    //验证失败
                    throw new Exception("表示层：数据验证无法通过，无法添加！");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ToUpdate(ModelStateDictionary model)
        {
            try
            {
                if (model.IsValid)
                {
                    BllApi<Entity>.Update(this);
                }
                else
                {
                    //验证失败
                    throw new Exception("表示层：数据验证无法通过，无法修改！");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ToDelete(ModelStateDictionary model)
        {
            try
            {
                if (model.IsValid)
                {
                    BllApi<Entity>.Delete(this);
                }
                else
                {
                    //验证失败
                    throw new Exception("表示层：数据验证无法通过，无法删除！");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ToAdd()
        {
            try
            {
                BllApi<Entity>.Add(this);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ToUpdate()
        {
            try
            {
                BllApi<Entity>.Update(this);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ToDelete()
        {
            try
            {
                BllApi<Entity>.Delete(this);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}