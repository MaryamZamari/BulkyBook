
using BulkyBook.Models;
using BulkyBookDataAccess;
using BulkyBookDataAccess.Repository;
using BulkyBookDataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    {

        private readonly IUnitOfWork _UnitOfWork;

    
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }


       
        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _UnitOfWork.CoverType.GetAll(); 
            return View(objCoverTypeList);
        }

       
        public IActionResult Create()
        {
            return View();
        }

        // the Created caegory _ POST
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public IActionResult Create(CoverType obj)
        {
          if (ModelState.IsValid)
            {
                
                _UnitOfWork.CoverType.Add(obj);
                _UnitOfWork.CoverType.Save();
                TempData["success"] = "CoverType created successfully";
                return RedirectToAction("Index");
            }
            //altrimenti
            return View(obj);
        }

       
        //EDIT_GET 
        public IActionResult Edit(int? id) 
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
           var coverTypeFromDb = _UnitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (coverTypeFromDb == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDb);
        }


        //EDIT  _ POST
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Edit(CoverType obj)
        {
           
            //ServerSide Validation : if model is valid
            if (ModelState.IsValid)
            {
                //----------------------------Update-----------------------------
             
                _UnitOfWork.CoverType.Update(obj);
                _UnitOfWork.CoverType.Save();
                return RedirectToAction("Index");
            }
            //altrimenti
            TempData["success"] = "CoverType updated successfully";
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
         
            var coverTypeFromDb = _UnitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
          
            if (coverTypeFromDb == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDb);
        }



        //DELETE _ POST
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public IActionResult DeletePOST(int? id)
        {
     
            var obj = _UnitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _UnitOfWork.CoverType.Remove(obj);
            _UnitOfWork.CoverType.Save();
            TempData["success"] = "CoverType deleted successfully";
            return RedirectToAction("Index");


        }


    }
}
