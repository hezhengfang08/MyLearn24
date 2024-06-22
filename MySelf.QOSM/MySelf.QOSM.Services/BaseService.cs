using MathNet.Numerics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using MySelf.QOSM.Common.Encrypt;
using MySelf.QOSM.Models.Context;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Services
{
    public class BaseService
    {
        public QOSMDbContext dbContext = null;
        public BaseService()
        {
            dbContext = new QOSMDbContext();
        }
        //提交方法
        public void Commit()
        {
            dbContext.SaveChanges();
        }
        /// <summary>
        /// 未提交的删除，可以批量之后一起提交
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void UnCommitDelete<T>(T t) where T : class
        {
            if (dbContext.Entry(t).State == EntityState.Detached)
            {
                dbContext.Set<T>().Attach(t);
                dbContext.Set<T>().Remove(t);
                //dbContext.Remove(t);
            }
        }
        public void DetachList<T>(List<T> list) where T : class
        {
            if (dbContext.Entry(list[0]).State != EntityState.Detached)
            {
                foreach (var t in list)
                {
                    Detach(t);
                }
            }
        }
        public void DeleteById<T>(long id) where T : class
        {
            T t = Find<T>(id);
            if (t == null) { throw new Exception($"{t} is null"); }
            dbContext.Set<T>().Remove(t);
            Commit();
        }
        //批量删除
        public void Delete<T>(IEnumerable<T> tList) where T : class
        {
            foreach (var t in tList)
            {
                dbContext.Set<T>().Attach(t);
            }
            dbContext.Set<T>().RemoveRange(tList);
            Commit();
        }
        public void Delete<T>(T t) where T: class
        {
            if(t==null)
            {
                throw new Exception($"{t} is null");
               
            }
            dbContext.Set<T>().Attach(t);
            dbContext.Set<T>().Remove(t);
            Commit();
        }
        /// <summary>
        /// 查找不跟踪状态
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find<T>(long id) where T : class
        {
            T t = dbContext.Set<T>().Find(id);
            if (t == null) { return null; }
            Detach(t);
            return t;
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T Insert<T>(T t) where T : class
        {
            dbContext.Set<T>().Add(t);
            Commit();
            return t;
        }
        /// <summary>
        /// 无跟踪查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcWhere"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IQueryable<T> Query<T> (Expression<Func<T, bool>> funcWhere)    where T : class
        {
            if (funcWhere == null)
            {
                throw new Exception("查询表达式不能为空");
            }
            return dbContext.Set<T>().AsNoTracking().Where(funcWhere);  
        }
      /// <summary>
      /// 无跟踪查询全部
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <returns></returns>
        public IQueryable<T> Query<T>() where T : class
        {
            return dbContext.Set<T>().AsNoTracking();
        }
        /// <summary>
        /// 修改单条
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <exception cref="Exception"></exception>
        public void Update<T>(T t) where T : class
        {
            if(t==null) { throw new Exception($"{t} is null"); }
            dbContext.Entry<T>(t).State = EntityState.Modified;
            Commit();

        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public void Update<T>(IEnumerable<T> list) where T : class 
        {
            foreach(var item in list)
            {
                dbContext.Set<T>().Attach(item);
                dbContext.Entry<T>(item).State = EntityState.Modified;
            }
            Commit();
        }
        public bool ExecuteTrans(Action excutAction, Action detachAction)
        {
            IDbContextTransaction tran = null;
            try
            {
                tran = dbContext.Database.BeginTransaction();//开启事务
                excutAction();
                Commit();
                tran.Commit();
                detachAction?.Invoke();
                return true;

            }catch(Exception ex)  {
                if (tran != null)
                    tran.Rollback();
                throw ; 

            }
            finally
            {
                tran.Dispose();
            }
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public IEnumerable<T> Insert<T>(IEnumerable<T> list) where T : class
        {
            dbContext.Set<T>().AddRange(list);
            Commit();   
            return list;
        }
        /// <summary>
        /// 取消跟踪
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void Detach<T>(T t) where T : class
        {
            if (t != null)
            {
                dbContext.Entry(t).State = EntityState.Detached;
            }
        }
        /// <summary>
        /// 生成密码的MD5字符串
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string GetMD5Str(string pwd)
        {
            if (pwd == null) return "";
            return MD5Encrypt.Encrypt(pwd);
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Dispose()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
