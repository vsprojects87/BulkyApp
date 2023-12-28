using Microsoft.AspNetCore.Mvc;

namespace BulkyApp.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
// First add the folder named 'Category' and add view in folder which is by default index.cshtml
// after adding view add the controller in controllers folder , add controller of same name as of view
// like for 'category' view add categorycontroller.cs controller, there must be controller name at the last