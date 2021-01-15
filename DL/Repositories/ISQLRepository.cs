using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DL.Repositories
{
    public interface ISQLRepository<T>
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        T Add(T entity);

        T Update(T entity);
        bool Remove(int id);

        IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate);
        int SaveChanges();

    }
}
