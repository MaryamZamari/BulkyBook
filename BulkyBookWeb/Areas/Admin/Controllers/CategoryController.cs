
using BulkyBook.Models;
using BulkyBookDataAccess;
using BulkyBookDataAccess.Repository;
using BulkyBookDataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {

        // private readonly ApplicationDbContext _db;       --------------> Replaced by CategoryRepository
        //private readonly ICategoryRepository _db;        --------------> Replaced by UnitOFWork
        private readonly IUnitOfWork _UnitOfWork;

        //all the string code of the connection string and bla bla bla to retrieve the data 
        // public CategoryController(ApplicationDbContext db)  //ctor tab tab            --------------> Replaced by UnitOFWork
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }


        //then we add model in the Index page(View) to be able to call it. here we only do the retrieving of the data. so it is not for the purpose of visualisation,
        //for visualisation we have to use the view. 
        public IActionResult Index()
        {
            // var objCategoryList = _db.Categories.ToList();
            IEnumerable<Category> objCategoryList = _UnitOfWork.Category.GetAll(); //_db.Categories;
            return View(objCategoryList);
        }

        //Method for going to the creation page
        public IActionResult Create()
        {
            return View();
        }

        //Method for posting the Created caegory _ POST
        [HttpPost]
        [ValidateAntiForgeryToken] //helps to prevent attacks. 
        public IActionResult Create(Category obj)
        {
            //Custom Validation
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Name and DisplayOrder can not be equal.");
            }
            //ServerSide Validation : if model is valid
            if (ModelState.IsValid)
            {
                //----------------------------aggiungi--------------
                //_db.Categories.Add(obj); 
                //_db.SaveChanges();       //after this, it will be effectively saved.


                //UOW --------------------> Replaced by UnitOFWork
                //_db.Add(obj);
                //_db.Save();
                _UnitOfWork.Category.Add(obj);
                _UnitOfWork.Category.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            //altrimenti
            return View(obj);
        }

        //############################################################################## ------- EDIT ---------- ###################################################
        //EDIT_GET method to find the selected category for EDITING
        public IActionResult Edit(int? id) //we put ? to say it is not nullable
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // var categoryFromDb = _UnitOfWork.Category.Find();--------------------> Replaced by 
            var categoryFromDb = _UnitOfWork.Category.GetFirstOrDefault(u => u.Id == id);//--------------------> Replaced by  UOW  _db.Categories.FirstOrDefault(u => u.Id == id);  
            //var categoryFromDb = _db.Categories.SingleOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }


        //EDIT _ Method for posting the Edited caegory _ POST
        [HttpPost]
        [ValidateAntiForgeryToken] //helps to prevent attacks. 
        public IActionResult Edit(Category obj)
        {
            //Custom Validation
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Name and DisplayOrder can not be equal.");
            }
            //ServerSide Validation : if model is valid
            if (ModelState.IsValid)
            {
                //----------------------------Update-----------------------------
                //_db.Categories.Update(obj);
                //_db.SaveChanges();       //after this, it will be effectively saved.

                //--------------------> Replaced by  UOW
                //_db.Update(obj);
                //_db.Save();
                _UnitOfWork.Category.Update(obj);
                _UnitOfWork.Category.Save();
                return RedirectToAction("Index");
            }
            //altrimenti
            TempData["success"] = "Category updated successfully";
            return View(obj);
        }

        //##########################################################------- DELETE --------##########################################################

        //Delete_GET method to find the selected category for EDITING
        public IActionResult Delete(int? id) //we put ? to say it is not nullable
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            // var categoryFromDb = _db.GetFirstOrDefault(u => u.Id == id); ------------------> Replaced by  UOW
            var categoryFromDb = _UnitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDb = _db.Categories.SingleOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }



        //DELETE _ POST
        [HttpPost]
        [ValidateAntiForgeryToken] //helps to prevent attacks. 
        public IActionResult DeletePOST(int? id)
        {
            //var obj = _db.Categories.Find(id);
            //   var obj = _db.GetFirstOrDefault(u => u.Id == id); ------------------> Replaced by UOW
            var obj = _UnitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            //delete
            //_db.Categories.Remove(obj);
            //_db.SaveChanges();       //after this, it will be effectively saved.
            // ------------------> Replaced by UOW
            //_db.Remove(obj);
            //_db.Save();
            _UnitOfWork.Category.Remove(obj);
            _UnitOfWork.Category.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");


        }


    }
}
