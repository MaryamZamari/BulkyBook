using BulkyBookDataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            //------------to make categories list and covertype list available for products 
            _db.Products.Include(u => u.Category);
            this.dbSet = _db.Set<T>(); 
        }


        public void Add(T entity)
        {
            dbSet.Add(entity); //added because had error for adding a covertype 
        }
        //includeProp("Category ,CoverType")
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(includeProperties != null)
            {
                foreach(var includeProp in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList(); 
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)   //- include properties to make categories list and covertype list available for products 
        {
            IQueryable<T> query = dbSet;
            //query = query.FirstOrDefault(filter);
            //return query.ToList();
            query= query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity); 
        }

        public void Remove(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);   
        }
    }
}
