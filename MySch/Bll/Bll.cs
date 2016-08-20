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
    /// 表示层 数据表 基类：
    /// 一、提供单数据记录的增、改、删操作
    /// 二、提供静态的单个、多重数据记录查询操作
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    public class Bll<Entity> where Entity : class
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
                    ApiBll<Entity>.Add(this);
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
                    ApiBll<Entity>.Update(this);
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
                    ApiBll<Entity>.Delete(this);
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
                ApiBll<Entity>.Add(this);
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
                ApiBll<Entity>.Update(this);
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
                ApiBll<Entity>.Delete(this);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        ///////////////////////////////////////////////////
        //////           以下是静态方法              //////
        ///////////////////////////////////////////////////

        /// <summary>
        /// JSON方式：将 实体对象 -> 表示数据
        /// 以ID主键查询方式获取实体对象
        /// </summary>
        /// <typeparam name="BllEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BllEntity GetEntity<BllEntity>(string id)
        {
            try
            {
                //根据主键查询数据层对象实体
                var entity = DataCRUD<Entity>.Entity(id);
                if (entity == null) throw new Exception("业务逻辑：未查到主键对应实体！");

                //再序列化成所要的对象
                return JsonApi<BllEntity>.JsonEntity(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// JSON方式：将 实体对象 -> 表示数据
        /// 条件Lambda查询获取单一实体对象
        /// </summary>
        /// <typeparam name="BllEntity"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static BllEntity GetEntity<BllEntity>(Expression<Func<Entity, bool>> func)
        {
            try
            {
                var entity = DataCRUD<Entity>.Entity(func);
                if (entity == null) throw new Exception("业务逻辑：无相关记录或存在多个实体！");

                return JsonApi<BllEntity>.JsonEntity(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// JSON方式：将 实体对象 -> 表示数据
        /// 条件Lambda查询获取多个实体对象
        /// </summary>
        /// <typeparam name="BllEntity"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static List<BllEntity> GetEntitys<BllEntity>(Expression<Func<Entity, bool>> func)
        {
            try
            {
                var entitys = DataCRUD<Entity>.Expression(func);
                return JsonApi<List<BllEntity>>.JsonEntity(entitys);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// JSON方式：将 实体对象 -> 表示数据
        /// 一、条件Lambda查询获取多个实体对象
        /// 二、以EasyUI的DataGrid的形式返回
        /// </summary>
        /// <typeparam name="BllEntity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public static object GetEntitysToDataGrid<BllEntity>(Expression<Func<Entity, bool>> where)
        {
            try
            {
                var entitys = DataCRUD<Entity>.Expression(where);
                //转换：实体对象 - 表示数据
                var entitys_bll = JsonApi<List<BllEntity>>.JsonEntity(entitys);
                //输出：转换成DataGrid的数据
                return EasyUI<BllEntity>.DataGrids(entitys_bll);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static object GetPagesToDataGrid<BllEntity, Key>(Expression<Func<Entity, bool>> where, Expression<Func<Entity, Key>> order, int pageIndex, int pageSize, OrderType ordertype)
        {
            try
            {
                int gets, total;
                //读取：分页实体对象
                var pages = ordertype == OrderType.ASC ? DataCRUD<Entity>.TakePage<Key>(where, order, pageIndex, pageSize, out  gets, out  total) : DataCRUD<Entity>.TakePageDesc<Key>(where, order, pageIndex, pageSize, out gets, out total);
                //转换：实体对象 - 表示数据
                var pages_bll = JsonApi<List<BllEntity>>.JsonEntity(pages);
                //输出：转换成DataGrid的数据
                return EasyUI<BllEntity>.DataGrids(pages_bll);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}