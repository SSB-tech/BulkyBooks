using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includedProperties = null);
        void Add(T entity);
        void Delete (T entity);
        void DeleteRange (IEnumerable<T> entities);
        T FindFirstOrDefault(Expression<Func<T, bool>> filter, string? includedProperties = null);
    }
}
