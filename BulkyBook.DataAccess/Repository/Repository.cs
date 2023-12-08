using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;

        internal DbSet<T> DbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.DbSet = _db.Set<T>(); 
        }
        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
           DbSet.RemoveRange(entities);
        }

        public T FindFirstOrDefault(Expression<Func<T, bool>> filter, string? includedProperties = null)
        {
            IQueryable<T> query = DbSet;
            query = query.Where(filter);
            if (includedProperties != null)
            {
                foreach (var includeProp in includedProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includedProperties = null)
        {
            IQueryable<T> query = DbSet;
            if(includedProperties != null)
            {
                foreach(var includeProp in includedProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                   query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }
    }
}
