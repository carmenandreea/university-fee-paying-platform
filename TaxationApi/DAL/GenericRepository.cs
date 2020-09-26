using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using TaxationApi.Models;
namespace TaxationApi.DAL
{
    public class GenericRepository<T> where T : class
    {
        public TaxationDbContext context;
        internal DbSet<T> dbSet;

        public GenericRepository(TaxationDbContext theContext)
        {
            this.context = theContext;
            this.dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public T GetEntityById(long id)
        {
            return dbSet.Find(id);
        }

        public void InsertEntity(T obj)
        {
            dbSet.AddAsync(obj);
        }

        public void DeleteEntity(long id)
        {
            T entity = dbSet.Find(id);
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public void UpdateEntity(T obj)
        {
            dbSet.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}
