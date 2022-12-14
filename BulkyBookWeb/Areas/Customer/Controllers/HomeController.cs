
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBookDataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _UnitOfWork; 

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _UnitOfWork = unitOfWork; 
        }

        //action methods 
        public IActionResult Index()
        {
            IEnumerable<Product> ProductList = _UnitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
            return View(ProductList);
        }

        //---------DETAILS 
        public IActionResult Details(int id)
        {
            ShoppingCard cardObj = new()
            {
                Count = 1,
                Product = _UnitOfWork.Product.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,CoverType")
            }
            return View(cardObj);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}