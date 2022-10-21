using BulkyBook.Models;
using BulkyBookDataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        //------------ Dependency Injection of the Db constructor ----------
        private ApplicationDbContext _db; 
        public CategoryRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        //------------ Dependency Injection of the Db constructor ----------
        //-----------------------------save will be handled by Unit of work ------------------------------- 
        public void Save()
        {
            _db.SaveChanges();
        }//to be commented

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
