using MySch.Bll.Custom;
using MySch.Core;
using MySch.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MySch.Bll
{
    public enum OrderType { ASC, DESC }

    /// <summary>
    /// 表示层 数据表 基类：功能：查询
    /// 一、提供静态的单个、多重数据记录查询操作
    /// 注意：本类静态方法，不直接引用，而是，实例方式引用
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    public class BllBase<Entity> where Entity : class
    {
        /// <summary>
        /// JSON方式：将 实体对象 -> 表示数据
        /// 以ID主键查询方式获取实体对象
        /// </summary>
        /// <typeparam name="BllEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        //public static BllEntity GetEntity<BllEntity>(string id)
        //{
        //    try
        //    {
        //        //根据主键查询数据层对象实体
        //        var entity = DataCRUD<Entity>.Entity(id);
        //        if (entity == null) throw new Exception("业务逻辑：未查到主键对应实体！");

        //        //再序列化成所要的对象
        //        return Jsons.JsonEntity<BllEntity>(entity);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        public static BllEntity GetEntity<BllEntity>(string id, string nullMessage)
        {
            try
            {
                //根据主键查询数据层对象实体
                var entity = DataCRUD<Entity>.Entity(id);
                if (entity == null) throw new Exception(nullMessage);

                //再序列化成所要的对象
                return Jsons.JsonEntity<BllEntity>(entity);
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
        public static BllEntity GetEntity<BllEntity>(Expression<Func<Entity, bool>> where)
        {
            try
            {
                var entity = DataCRUD<Entity>.Entity(where);
                if (entity == null) throw new Exception("业务逻辑：无相关记录或存在多个实体！");

                return Jsons.JsonEntity<BllEntity>(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static BllEntity GetEntityOrDefault<BllEntity>(Expression<Func<Entity, bool>> where)
        {
            try
            {
                var entity = DataCRUD<Entity>.Entity(where);
                return Jsons.JsonEntity<BllEntity>(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static BllEntity GetEntity<BllEntity>(Expression<Func<Entity, bool>> where, string nullMessage)
        {
            try
            {
                var entity = DataCRUD<Entity>.Entity(where);
                if (entity == null) throw new Exception(nullMessage);

                return Jsons.JsonEntity<BllEntity>(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static BllEntity GetEntityExists<BllEntity>(Expression<Func<Entity, bool>> where, string existMessage)
        {
            try
            {
                var entity = DataCRUD<Entity>.Entity(where);
                if (entity != null) throw new Exception(existMessage);

                return Jsons.JsonEntity<BllEntity>(entity);
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
        public static IEnumerable<BllEntity> GetEntitys<BllEntity>(Expression<Func<Entity, bool>> where)
        {
            try
            {
                var entitys = DataCRUD<Entity>.Entitys(where);
                return Jsons.JsonEntity<IEnumerable<BllEntity>>(entitys);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static IEnumerable<object> GetEntitys(Expression<Func<Entity, bool>> where, string fieldName)
        {
            try
            {
                var entitys = DataCRUD<Entity>.Entitys(where);
                var entitys_fields = from entity in entitys
                                     select new
                                     {
                                         fieldName = ReObject.Entity(entity, fieldName),
                                     };
                return entitys_fields;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int Count(Expression<Func<Entity, bool>> where)
        {
            try
            {
                return DataCRUD<Entity>.Count(where);
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
        public static object GetDataGridEntitys<BllEntity>(Expression<Func<Entity, bool>> where)
        {
            try
            {
                var entitys = DataCRUD<Entity>.Entitys(where);
                //转换：实体对象 - 表示数据
                var entitys_bll = Jsons.JsonEntity<List<BllEntity>>(entitys);
                //输出：转换成DataGrid的数据
                return EasyUI<BllEntity>.DataGrids(entitys_bll, entitys.Count());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// JSON方式：将 实体对象 -> 表示数据
        /// </summary>
        /// <typeparam name="BllEntity"></typeparam>
        /// <typeparam name="Key">排序字段</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="ordertype">排序方式</param>
        /// <returns></returns>
        private static object GetDataGridPagesPrivate<BllEntity, Key>(Expression<Func<Entity, bool>> where, Expression<Func<Entity, Key>> order, int pageIndex, int pageSize, OrderType ordertype)
        {
            try
            {
                int gets, total;
                //读取：分页实体对象
                var pages = ordertype == OrderType.ASC ?
                    DataCRUD<Entity>.TakePage<Key>(where, order, pageIndex, pageSize, out gets, out total) :
                    DataCRUD<Entity>.TakePageDesc<Key>(where, order, pageIndex, pageSize, out gets, out total);

                //转换：实体对象 - 表示数据
                var pages_bll = Jsons.JsonEntity<List<BllEntity>>(pages);

                //输出：转换成DataGrid的数据
                return EasyUI<BllEntity>.DataGrids(pages_bll, total);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static object GetDataGridPages<BllEntity, Key>(Expression<Func<Entity, bool>> where, Expression<Func<Entity, Key>> order, int pageIndex, int pageSize)
        {
            try
            {
                return GetDataGridPagesPrivate<BllEntity, Key>(where, order, pageIndex, pageSize, OrderType.ASC);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static object GetDataGridPagesDesc<BllEntity, Key>(Expression<Func<Entity, bool>> where, Expression<Func<Entity, Key>> order, int pageIndex, int pageSize)
        {
            try
            {
                return GetDataGridPagesPrivate<BllEntity, Key>(where, order, pageIndex, pageSize, OrderType.DESC);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static object GetTrees<BllEntity, Key>(Expression<Func<Entity, bool>> where, string ids, string name, string state, string memo) where BllEntity : class
        {
            var entitys = DataCRUD<Entity>.Entitys(where);
            //转换：实体对象 - 表示数据
            var entitys_bll = Jsons.JsonEntity<List<BllEntity>>(entitys);
            //输出：树Json
            return EasyUITree.ToTreeJsons<BllEntity>(entitys_bll, ids, name, state, memo);
        }
    }
}