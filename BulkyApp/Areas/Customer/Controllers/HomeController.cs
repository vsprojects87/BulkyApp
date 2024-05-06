using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using BulkyApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;

		public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
        {
			IEnumerable<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
			return View(objProductList);
        }
        // here returning the list thats why IEnumerable<>

        [ActionName("Details")]
        public IActionResult Details(int id)
            // id parameter must be same name as mention in asp-route-id, if asp-route-pid then parameter
            // should be int pid
        {
            Product product = _unitOfWork.Product.Get(u=>u.Id==id, includeProperties: "Category");
            return View(product);
		}
        // here returning a single record thats why Product product

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
