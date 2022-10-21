using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository.IRepository
{
   public interface IRepository<T> where T: class
    {
        //T-Category
        T GetFirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> filter, string? includeProperties = null); //include Properties to add category/covertype for products
        IEnumerable<T> GetAll(string? includeProperties = null); //include Properties to add category/covertype for products
        void Add(T entity);

        void Remove(T entity);

        void Remove(IEnumerable<T> entity);
    }
}
