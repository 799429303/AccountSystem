using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace DataBase
{
  public  interface IRepositoryBase<T> where T : class
    {
        IRepositoryBase<T> BeginTrans();
        int Commit();
        int AddEntity(T entity) ;
        int DeleteEntity(T entity) ;
        int DeleteEntity(string id);
        int ModifyEntity(T entity) ;
        T FindEntity(object id) ;
        T FindEntity(Expression<Func<T, bool>> predicate);
        List<T> FindEntities(Expression<Func<T, bool>> predicate);
    }
}
