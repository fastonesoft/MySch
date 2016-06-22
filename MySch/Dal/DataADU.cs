using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySch.Dal
{
    /// <summary>
    /// 本类当中公涉及实体数据的添加、删除、修改操作
    /// </summary>
    /// <typeparam name="TEntity">实体类</typeparam>
    public class DataADU<TEntity> where TEntity : class
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
            catch (Exception e)
            {
                throw e;
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
            catch (Exception e)
            {
                throw e;
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
                    if (db.Entry<TEntity>(entity).State == EntityState.Detached)
                    {
                        db.Entry<TEntity>(entity).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="modelstate">模型状态</param>
        /// <param name="entity">数据实体</param>
        /// <returns></returns>
        public static void Add(ModelStateDictionary modelstate, TEntity entity)
        {
            if (modelstate.IsValid)
            {
                //验证通过、添加数据
                Add(entity);
            }
            else
            {
                //验证失败
                throw new Exception("验证：数据无法通过，不能添加！");
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="modelstate">数据模型</param>
        /// <param name="entity">数据实体</param>
        /// <returns></returns>
        public static void Delete(ModelStateDictionary modelstate, TEntity entity)
        {
            if (modelstate.IsValid)
            {
                //验证通过、提交删除
                Delete(entity);
            }
            else
            {
                //验证失败
                throw new Exception("验证：数据无法通过，不能删除！");
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="modelstate">模型状态</param>
        /// <param name="entity">模型实体</param>
        /// <returns></returns>
        public static void Update(ModelStateDictionary modelstate, TEntity entity)
        {
            if (modelstate.IsValid)
            {
                //验证通过、提交修改
                Update(entity);
            }
            else
            {
                //验证失败
                throw new Exception("验证：数据无法通过，不能修改！");
            }
        }
    }

}