using BulkyBook.Models;
using BulkyBookDataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        //------------ Dependency Injection of the Db constructor ----------
        private ApplicationDbContext _db; 
        public ProductRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
       
        public void Save()
        {
            _db.SaveChanges();
        }//to be commented

        public void Update(Product obj)
        {
            /* _db.Products.Update(obj);*/ //products is in the applicationDbContext
            var objFromDb = _db.Products.FirstOrDefault(u=>u.Id==obj.Id); 
            if(objFromDb != null)
            {   //u can restrict what the user can edit or what not
                objFromDb.Title = obj.Title;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Price = obj.Price;
                objFromDb.Price50 = obj.Price50;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price100 = obj.Price100;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.CoverTypeId = obj.CoverTypeId;
                objFromDb.Author = obj.Author;
                if(obj.ImageUrl != null)  //will be modified only if there is already an image url
                {
                    objFromDb.ImageUrl= obj.ImageUrl;
                }
            }
            
        }
    }
}
