using BulkyBook.Models;

namespace BulkyBookDataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
        void Save();

   
    }
}