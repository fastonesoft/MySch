using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Dal
{
    #region DataTake   实体分页、条件查询通用类

    /// <summary>
    /// 涉及分页数据查询、条件查询相关操作
    /// </summary>
    /// <typeparam name="TEntity">实体类</typeparam>
    /// <typeparam name="TKey">排序字段</typeparam>
    public class DataTake<TEntity> where TEntity : class
    {
        /// <summary>
        /// 根据Lamda查询条件读取第X页的数据（升序）
        /// </summary>
        /// <param name="whereFunc">Lamda查询条件</param>
        /// <param name="orderFunc">Lamda排序条件</param>
        /// <param name="pagesize">每页数据</param>
        /// <param name="pageindex">页码</param>
        /// <param name="getmany">获得实际数据值</param>
        /// <returns></returns>
        public static IEnumerable<TEntity> TakePage<TKey>(Expression<Func<TEntity, bool>> whereFunc, Expression<Func<TEntity, TKey>> orderFunc, int page, int rows, out int many, out int total)
        {
            using (BaseContext db = new BaseContext())
            {
                rows = rows <= 0 ? 10 : rows;
                int skip = (page - 1) * rows;
                skip = skip < 0 ? 0 : skip;

                var list = db.Set<TEntity>().Where(whereFunc).OrderBy(orderFunc).Skip(skip).Take(rows);
                many = list.Count();
                total = db.Set<TEntity>().Count(whereFunc);

                return list.ToList();
            }
        }

        /// <summary>
        /// 根据Lamda查询条件读取第X页的数据（降序）
        /// </summary>
        /// <param name="whereFunc">Lamda查询条件</param>
        /// <param name="orderFunc">Lamda排序条件</param>
        /// <param name="pagesize">每页数据</param>
        /// <param name="pageindex">页码</param>
        /// <param name="getmany">获得实际数据值</param>
        /// <returns></returns>
        public static IEnumerable<TEntity> TakePageDesc<TKey>(Expression<Func<TEntity, bool>> whereFunc, Expression<Func<TEntity, TKey>> orderFunc, int page, int rows, out int many, out int total)
        {
            using (BaseContext db = new BaseContext())
            {
                rows = rows <= 0 ? 10 : rows;
                int skip = (page - 1) * rows;
                skip = skip < 0 ? 0 : skip;

                var list = db.Set<TEntity>().Where(whereFunc).OrderByDescending(orderFunc).Skip(skip).Take(rows);
                many = list.Count();
                total = db.Set<TEntity>().Count(whereFunc);

                return list.ToList();
            }
        }

        /// <summary>
        /// 根据Lamda查询条件读取数据（升序）
        /// </summary>
        /// <param name="whereFunc">Lamda查询条件</param>
        /// <param name="orderFunc">Lamda排序条件</param>
        /// <returns></returns>
        public static IEnumerable<TEntity> TakeOrder<TKey>(Expression<Func<TEntity, bool>> whereFunc, Expression<Func<TEntity, TKey>> orderFunc)
        {
            using (BaseContext db = new BaseContext())
            {
                return db.Set<TEntity>().Where(whereFunc).OrderBy(orderFunc).ToList();
            }
        }

        /// <summary>
        /// 根据Lamda查询条件读取数据（降序）
        /// </summary>
        /// <param name="whereFunc">Lamda查询条件</param>
        /// <param name="orderFunc">Lamda排序条件</param>
        /// <returns></returns>
        public static IEnumerable<TEntity> TakeOrderDesc<TKey>(Expression<Func<TEntity, bool>> whereFunc, Expression<Func<TEntity, TKey>> orderFunc)
        {
            using (BaseContext db = new BaseContext())
            {
                return db.Set<TEntity>().Where(whereFunc).OrderByDescending(orderFunc).ToList();
            }
        }
    }
    #endregion
}