using MySch.Dal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MySch.Bll
{
    /// <summary>
    /// 主要功能：完成 表示层数据 -> 实体类对象 转换 及 增、改、删 操作
    /// 一、添加：直接将表示数据以JSON方式转换为实体对象
    /// 二、修改：反射出表示数据的主键，用以查询实体对象，匹配数据、对象关键字，
    ///           吻合，传输其他字段属性，提交实体层，完成修改
    /// 三、删除：基本同上
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    public class BllApi<Entity> where Entity : class
    {
        /// <summary>
        /// JSON方式： 将 表示数据 -> 实体对象
        /// </summary>
        /// <param name="obj">表示数据</param>
        /// <returns></returns>
        public static void Add(object obj)
        {
            try
            {
                //表示数据 -> 实体对象
                var entity = Jsons.JsonEntity<Entity>(obj);
                //实体对象：添加
                DataCRUD<Entity>.Add(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 反射方式：将 表示数据 -> 实体对象
        /// </summary>
        /// <param name="obj">表示数据</param>
        /// <returns></returns>
        public static void Update(object obj)
        {
            //一、获取表示数据 主键
            //二、查询实体对象
            //三、遍历表示数据 属性 -> 实体对象 赋值
            try
            {
                //反射获取表示数据主键
                Type obj_type = obj.GetType();
                var obj_props = obj_type.GetProperties();
                var obj_id = obj_props.FirstOrDefault(a => a.Name == "ID");
                var obj_ids = obj_props.FirstOrDefault(a => a.Name == "IDS");
                var obj_ids_value = obj_ids.GetValue(obj);
                if (obj_ids == null) throw new Exception("业务逻辑：表示数据未设置主键！");

                //根据主键查询数据层对象实体
                var entity = DataCRUD<Entity>.Entity(obj_id.GetValue(obj));
                if (entity == null) throw new Exception("业务逻辑：未查到表示数据对应实体！");

                //反射获取实体对象
                Type entity_type = entity.GetType();
                var entity_props = entity_type.GetProperties();
                var entity_ids = entity_props.FirstOrDefault(a => a.Name == "IDS");
                var entity_ids_value = entity_ids.GetValue(entity);

                //检测是否对应
                if (obj_ids_value.Equals(entity_ids_value))
                {
                    //遍历：表示数据
                    foreach (var obj_p in obj_props)
                    {
                        //字段：非主键，逐一赋值
                        string name = obj_p.Name;
                        if (name != "ID" && name != "IDS")
                        {
                            object value = obj_p.GetValue(obj);
                            var entity_p = entity_props.FirstOrDefault(a => a.Name == name);
                            //实体对象：存在字段，接收；不存在，PASS
                            if (entity_p != null)
                            {
                                //赋值
                                entity_p.SetValue(entity, value);
                            }
                        }
                    }
                    //实体对象：修改
                    DataCRUD<Entity>.Update(entity);
                }
                else
                {
                    throw new Exception("业务逻辑：表示数据转换实体对象失败，无法修改！");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 反射方式：将 表示数据 -> 实体对象
        /// </summary>
        /// <param name="obj">表示数据</param>
        /// <returns></returns>
        public static void Delete(object obj)
        {
            //要确认 表示数据 与 实体对象 的ID、IDS完全对应
            try
            {
                //反射获取表示数据、主键、编号
                Type obj_type = obj.GetType();
                var obj_props = obj_type.GetProperties();
                var obj_id = obj_props.FirstOrDefault(a => a.Name == "ID");
                var obj_ids = obj_props.FirstOrDefault(a => a.Name == "IDS");
                var obj_ids_value = obj_ids.GetValue(obj);
                if (obj_ids == null) throw new Exception("业务逻辑：表示数据未设置主键！");

                //根据主键查询实体对象
                var entity = DataCRUD<Entity>.Entity(obj_id.GetValue(obj));
                if (entity == null) throw new Exception("业务逻辑：未查到表示数据对应实体！");

                //反射获取实体对象、编号
                Type entity_type = entity.GetType();
                var entity_props = entity_type.GetProperties();
                var entity_ids = entity_props.FirstOrDefault(a => a.Name == "IDS");
                var entity_ids_value = entity_ids.GetValue(entity);

                //检测：表示数据、实体对象 编号 是否吻合
                if (obj_ids_value.Equals(entity_ids_value))
                {
                    //实体对象：删除
                    DataCRUD<Entity>.Delete(entity);
                }
                else
                {
                    throw new Exception("业务逻辑：表示数据转换实体对象失败，无法删除！");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}