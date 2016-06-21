using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MySch.Dal
{

    /// <summary>
    /// 本类当中所有查询，均为简单查询
    /// 不涉及复杂条件设置
    /// 复杂查询请参照：DoTake类
    /// </summary>
    /// <typeparam name="TEntity">实体类</typeparam>
    public class DataQuery<TEntity> where TEntity : class
    {
        /// <summary>
        /// 默认排序方式，查询所有数据
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TEntity> All()
        {
            using (BaseContext db = new BaseContext())
            {
                return db.Set<TEntity>().ToList();
            }
        }

        public static int Count(Expression<Func<TEntity, bool>> whereFunc)
        {
            using (BaseContext db = new BaseContext())
            {
                return db.Set<TEntity>().Count(whereFunc);
            }

        }

        /// <summary>
        /// 根据表达式查询记录
        /// </summary>
        /// <param name="whereFunc"></param>
        /// <returns></returns>
        public static IEnumerable<TEntity> Expression(Expression<Func<TEntity, bool>> whereFunc)
        {
            using (BaseContext db = new BaseContext())
            {
                return db.Set<TEntity>().Where(whereFunc).ToList();
            }
        }

        /// <summary>
        /// 根据主键id查询数据
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public static TEntity Entity(params object[] id)
        {
            using (BaseContext db = new BaseContext())
            {
                return db.Set<TEntity>().Find(id);
            }
        }

        /// <summary>
        /// 根据表达式查询记录，如果找到唯一记录，则，返回记录本身；否则为：空
        /// </summary>
        /// <param name="whereFunc"></param>
        /// <returns></returns>
        public static TEntity Entity(Expression<Func<TEntity, bool>> whereFunc)
        {
            using (BaseContext db = new BaseContext())
            {
                var res = db.Set<TEntity>().Where(whereFunc);
                return res.Count() == 1 ? res.Single() : null;
            }
        }

        public static TEntity Max()
        {
            using (BaseContext db = new BaseContext())
            {
                return db.Set<TEntity>().AsQueryable().Max();
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
        /// 检查数据内容是否存在
        /// </summary>
        /// <param name="whereFunc">查询条件表达式</param>
        /// <param name="count">返回查询记录数</param>
        /// <returns></returns>
        public static bool CheckExists(Expression<Func<TEntity, bool>> whereFunc, out int count)
        {
            using (BaseContext db = new BaseContext())
            {
                var res = db.Set<TEntity>().Where(whereFunc);
                count = res.Count();
                return count != 0;
            }
        }

        /// <summary>
        /// 检查数据内容是否存在
        /// </summary>
        /// <param name="whereFunc">查询条件表达式</param>
        /// <returns></returns>
        public static bool CheckExists(Expression<Func<TEntity, bool>> whereFunc)
        {
            int count = 0;
            return CheckExists(whereFunc, out count);
        }

        /// <summary>
        /// 检查数据记录是否唯一
        /// </summary>
        /// <param name="whereFunc">查询条件表达式</param>
        /// <returns></returns>
        public static bool CheckUnique(Expression<Func<TEntity, bool>> whereFunc)
        {
            using (BaseContext db = new BaseContext())
            {
                return db.Set<TEntity>().Count(whereFunc) == 1;
            }
        }
    }

}