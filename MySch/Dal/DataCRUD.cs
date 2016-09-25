using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Dal
{
    /// <summary>
    /// 本类当中公涉及实体数据的添加、删除、修改操作
    /// </summary>
    /// <typeparam name="TEntity">实体类</typeparam>
    public class DataCRUD<TEntity> where TEntity : class
    {
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">数据实体</param>
        public static void Add(TEntity entity)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    db.Set<TEntity>().Add(entity);
                    db.SaveChanges();
                }
            }
            catch
            {
                throw new Exception("数据层：实体数据无法添加！");
            }
        }

        public static void Add(IEnumerable<TEntity> entitys)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    db.Set<TEntity>().AddRange(entitys);
                    db.SaveChanges();
                }
            }
            catch
            {
                throw new Exception("数据层：实体数据无法添加！");
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity">数据实体</param>
        public static void Update(TEntity entity)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    db.Entry<TEntity>(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch
            {
                throw new Exception("数据层：实体数据无法更新！");
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">数据实体</param>
        public static void Delete(TEntity entity)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    db.Entry<TEntity>(entity).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch
            {
                throw new Exception("数据层：实体数据无法删除！");
            }
        }

        /////////////////////////
        ////       查询      ////
        /////////////////////////

        /// <summary>
        /// 表达式统计
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static int Count(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    return db.Set<TEntity>().Count(where);
                }
            }
            catch (Exception)
            {
                throw new Exception("数据层：表达式统计，异常！");
            }
        }

        public static TResult Max<TResult>(Expression<Func<TEntity, bool>> where, Func<TEntity, TResult> max)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    return db.Set<TEntity>().Where(where).Max<TEntity, TResult>(max);
                }
            }
            catch (Exception)
            {
                throw new Exception("数据层：Max查询，异常！");
            }
        }

        /// <summary>
        /// 表达式查询
        /// 根据Lambda查询数据集
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static IEnumerable<TEntity> Entitys(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    return db.Set<TEntity>().Where(where).ToList();
                }
            }
            catch
            {
                throw new Exception("数据层：表达式查询，异常！");
            }
        }

        /// <summary>
        /// 主键实体查询
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public static TEntity Entity(params object[] id)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    return db.Set<TEntity>().Find(id);
                }
            }
            catch
            {
                throw new Exception("数据层：主键实体查询，出错！");
            }
        }

        /// <summary>
        /// 表达式实体查询
        /// 根据表达式查询记录，如果找到唯一记录，则，返回记录本身；否则为：空
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static TEntity Entity(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    var res = db.Set<TEntity>().Where(where);
                    return res.Count() == 1 ? res.Single() : null;
                }
            }
            catch
            {
                throw new Exception("数据层：表达式实体查询，出错！");
            }
        }

        ///// <summary>
        ///// 用SQL参数式查询数据
        ///// </summary>
        ///// <param name="sql">带参数的SQL查询</param>
        ///// <param name="parameters">参数列表</param>
        ///// <returns></returns>
        //public static IEnumerable<TEntity> FindByParams(string sql, params object[] parameters)
        //{
        //    using (BaseContext db = new BaseContext())
        //    {
        //        return db.Set<TEntity>().SqlQuery(sql, parameters).ToList();

        //        //参数模拟使用方法
        //        //SqlParameter sp1 = new SqlParameter("@ID", SqlDbType.NVarChar, 20);
        //        //SqlParameter sp2 = new SqlParameter("@Name", SqlDbType.NVarChar, 20);
        //        //return db.Set<TEntity>().SqlQuery(sql, sp1, sp2).ToList();
        //    }
        //}


        /// <summary>
        /// 分布查询（升）
        /// 根据Lambda查询条件读取第X页的数据（升序）
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="where">Lambda查询条件</param>
        /// <param name="order">Lambda排序条件</param>
        /// <param name="pageIndex">数据页索引</param>
        /// <param name="pageSize">数据页大小</param>
        /// <param name="gets">实际读取记录数</param>
        /// <param name="total">查询数据总数</param>
        /// <returns></returns>
        public static IEnumerable<TEntity> TakePage<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order, int pageIndex, int pageSize, out int gets, out int total)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    int skip = (pageIndex - 1) * pageSize;
                    var list = db.Set<TEntity>().Where(where).OrderBy(order).Skip(skip).Take(pageSize);
                    gets = list.Count();
                    total = db.Set<TEntity>().Count(where);

                    return list.ToList();
                }
            }
            catch
            {
                throw new Exception("数据层：分布查询（升），出错！");
            }
        }

        /// <summary>
        /// 分布查询（降）
        /// 根据Lambda查询条件读取第X页的数据（降序）
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="where">Lambda查询条件</param>
        /// <param name="order">Lambda排序条件</param>
        /// <param name="pageIndex">数据页索引</param>
        /// <param name="pageSize">数据页大小</param>
        /// <param name="gets">实际读取记录数</param>
        /// <param name="total">查询数据总数</param>
        /// <returns></returns>
        public static IEnumerable<TEntity> TakePageDesc<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order, int pageIndex, int pageSize, out int gets, out int total)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    int skip = (pageIndex - 1) * pageSize;
                    var list = db.Set<TEntity>().Where(where).OrderByDescending(order).Skip(skip).Take(pageSize);
                    gets = list.Count();
                    total = db.Set<TEntity>().Count(where);

                    return list.ToList();
                }
            }
            catch
            {
                throw new Exception("数据层：分布查询（降），出错！");
            }
        }

        /// <summary>
        /// 排序查询（升）
        /// 根据Lamda查询条件读取数据（升序）
        /// </summary>
        /// <param name="where">Lamda查询条件</param>
        /// <param name="order">Lamda排序条件</param>
        /// <returns></returns>
        public static IEnumerable<TEntity> TakeOrder<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    return db.Set<TEntity>().Where(where).OrderBy(order).ToList();
                }
            }
            catch
            {
                throw new Exception("数据层：排序查询（升），出错！");
            }
        }

        /// <summary>
        /// 根据Lamda查询条件读取数据（降序）
        /// </summary>
        /// <param name="where">Lamda查询条件</param>
        /// <param name="order">Lamda排序条件</param>
        /// <returns></returns>
        public static IEnumerable<TEntity> TakeOrderDesc<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order)
        {
            try
            {
                using (BaseContext db = new BaseContext())
                {
                    return db.Set<TEntity>().Where(where).OrderByDescending(order).ToList();
                }
            }
            catch
            {
                throw new Exception("数据层：排序查询（降），出错！");
            }
        }
    }

}