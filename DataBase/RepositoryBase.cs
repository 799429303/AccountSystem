using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Data.Entity.Infrastructure;
using System.Data.Common;
using System.Data;

namespace DataBase
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {

        public AccountSysEntities dbContext = new AccountSysEntities();
        private DbTransaction dbTransaction { get; set; }
        public IRepositoryBase<T> BeginTrans()
        {
            DbConnection dbConnection = ((IObjectContextAdapter)dbContext).ObjectContext.Connection;
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            dbTransaction = dbConnection.BeginTransaction();
            return this;
        }
        public int Commit()
        {
            try
            {
                var returnValue = dbContext.SaveChanges();
                if (dbTransaction != null)
                {
                    dbTransaction.Commit();
                }
                return returnValue;
            }
            catch (Exception)
            {
                if (dbTransaction != null)
                {
                    this.dbTransaction.Rollback();
                }
                throw;
            }
            finally
            {
                this.Dispose();
            }
        }
        public void Dispose()
        {
            if (dbTransaction != null)
            {
                this.dbTransaction.Dispose();
            }
            this.dbContext.Dispose();
        }
        public int AddEntity(T entity)
        {
            dbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Added;
            return dbContext.SaveChanges();
        }
        public int DeleteEntity(T entity) 
        {
            dbContext.Set<T>().Attach(entity);
            dbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            return dbContext.SaveChanges();
        }
        public int ModifyEntity(T entity)
        {
            dbContext.Set<T>().Attach(entity);
            PropertyInfo[] props = entity.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(entity, null) != null)
                {
                    if (prop.GetValue(entity, null).ToString() == "&nbsp;")
                        dbContext.Entry(entity).Property(prop.Name).CurrentValue = null;
                    dbContext.Entry(entity).Property(prop.Name).IsModified = true;
                }
            }
            return dbContext.SaveChanges();
        }
        public T FindEntity(object id)
        {
            return dbContext.Set<T>().Find(id);
        }
        public T FindEntity(Expression<Func<T, bool>> predicate) 
        {
            return dbContext.Set<T>().FirstOrDefault(predicate);
        }
        public List<T> FindEntities(Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().Where(predicate).ToList<T>();
        }
    }
}
