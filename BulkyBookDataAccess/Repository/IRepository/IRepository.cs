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
        T GetFirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAll();
        void Add(T entity);

        void Remove(T entity);

        void Remove(IEnumerable<T> entity);
    }
}
