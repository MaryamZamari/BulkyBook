using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository.IRepository
{
    //here we will have the methods specificly for the Category. the ones that can nt be as generic as the ones of the IRepository
    public interface ICategoryRepository: IRepository<Category> 
    {
        void Update(Category obj);
        void Save();
    }
}
