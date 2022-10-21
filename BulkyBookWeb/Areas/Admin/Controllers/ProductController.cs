
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBookDataAccess;
using BulkyBookDataAccess.Repository;
using BulkyBookDataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment; //for creating the first product


        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment) //for creating the first product
        {
            _UnitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }


       
        public IActionResult Index()
        {
          //  IEnumerable<Product> objProductList = _UnitOfWork.Product.GetAll();    ----> commented since i'll call API in order to show the product list.  
            return View();
        }
        //--------------------------------------------------------Instead of Create and Edit, we add Upsert
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// the Created caegory _ POST
        //[HttpPost]
        //[ValidateAntiForgeryToken]  
        //public IActionResult Create(Product obj)
        //{
        //  if (ModelState.IsValid)
        //    {

        //        _UnitOfWork.Product.Add(obj);
        //        _UnitOfWork.Product.Save();
        //        TempData["success"] = "Product created successfully";
        //        return RedirectToAction("Index");
        //    }
        //    //altrimenti
        //    return View(obj);
        //}
        //--------------------------------------------------------Instead of Create and Edit, we add Upsert

        //Upsert_GET 
        public IActionResult Upsert(int? id) 
        {//this is a tightly binded view with the model, using ProductVM
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _UnitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _UnitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };
            Console.WriteLine(productVM);
            if (id == null || id == 0) //-------> If the prod doesnt exist,Create product
            {  
                //ViewBag.CategoryList= CategoryList;
                ////------Later see how to manage by ViewData
                //ViewBag.CoverTypeList= CoverTypeList;
                //return View(product);
                return View(productVM);
            }
            else
            {
                //Update product
            }
            return View();
        }


        //EDIT_POST
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Upsert(ProductVM obj, IFormFile? file)  //IFormFile serves for uploading the image  "?" makes it nullable . I just didnt understand where i got the IFormFile(is it a default type?)
        {
            //ServerSide Validation : if model is valid
            if (ModelState.IsValid) //-------> If the prod doesnt exist,Create product
            {
                //Create image and upload in the folder
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if(file != null) //would mean that the image is already uploaded so it would be an update. 
                {
                    string fileName= Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);
                    //to find the path of the file(fileStreams) and create it 
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams); //copy the created file in the path 
                    }
                    obj.Product.ImageUrl= @"images\products" + fileName + extension;  //the image URL associated to the product, to be saved in the db
                }
                //Create
                _UnitOfWork.Product.Add(obj.Product);
                _UnitOfWork.Product.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            //altrimenti
            
            return View(obj);
        }

        //------- DELETE 

        //Delete_GET 
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var productFromDb = _UnitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }



        //DELETE _ POST
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public IActionResult DeletePOST(int? id)
        {
            var obj = _UnitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _UnitOfWork.Product.Remove(obj);
            _UnitOfWork.Product.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _UnitOfWork.Product.GetAll(includeProperties: "Category");                  //include category is to get categories of each product. 
            return Json(new { data= productList });
        }
        #endregion


    }
}
