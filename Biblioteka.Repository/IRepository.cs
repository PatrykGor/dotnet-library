using Biblioteka.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Biblioteka.Repository 
{ 
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T,bool>> query);
        IEnumerable<T> GetMultiple(Expression<Func<T,bool>> query);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void SaveChanges();
    }
}
