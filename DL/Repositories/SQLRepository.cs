using DL.Context;
using DL.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DL.Repositories
{
    public class SQLRepository<T> : ISQLRepository<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _dbSet;

        public SQLRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T Add(T entity)
        {
            var entryEntity = _dbSet.Add(entity);
            //_context.SaveChanges();
            return entryEntity.Entity;
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public bool Remove(int id)
        {
            var entity = GetById(id);
            var entryEntity = _dbSet.Remove(entity);
            if (entryEntity != null)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public T Update(T entity)
        {
            var entryEntity = _dbSet.Update(entity);
            //_context.SaveChanges();
            return entryEntity.Entity;
        }

        public int SaveChanges() {
            var rows = _context.SaveChanges();
            return rows;
        }

        public IQueryable<T> FindByInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes)
        {
            return _dbSet.IncludeMultiple(includes).Where(predicate).AsQueryable();
        }


    }
}
