using BulkyApp.Models;
using BulkyApp.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using BulkyApp.DataAccess.Repository.IRepository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bulky.Models.ViewModels;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        // 1
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            // 2
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll()
                    .Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.CategoryId.ToString()
                    });
                ProductVM productVM = new ProductVM
                {
                    CategoryList = CategoryList,
                    Product = new Product()
                };
                return View(productVM);

        }



    [HttpPost]
    public IActionResult Create(Product obj)
    {
        //3
        if (ModelState.IsValid)
        {
            _unitOfWork.Product.Add(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Product Created Successfully";
            return RedirectToAction("Index");
        }
        return View();
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
        //Product? productFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
        //Product? productFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

        if (productFromDb == null)
        {
            return NotFound();
        }
        return View(productFromDb);
    }
    [HttpPost]
    public IActionResult Edit(Product obj)
    {

        if (ModelState.IsValid)
        {
            _unitOfWork.Product.Update(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Product Updated Successfully";
            return RedirectToAction("Index");
        }
        return View();
    }


    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Product? ProductFromDb = _unitOfWork.Product.Get(u => u.Id == id);
        //Product? productFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
        //Product? productFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

        if (ProductFromDb == null)
        {
            return NotFound();
        }
        return View(ProductFromDb);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
        if (obj == null)
        {
            return NotFound();
        }
        _unitOfWork.Product.Delete(obj);
        _unitOfWork.Save();
        TempData["Success"] = "Product Deleted Successfully";
        return RedirectToAction("Index");

    }

}
}
// First add the folder named 'Product' and add view in folder which is by default index.cshtml
// after adding view add the controller in controllers folder , add controller of same name as of view
// like for 'product' view add productcontroller.cs controller, there must be controller name at the last

// 1 ->
// In program.cs file which consists of all configuration info we have already configured database
// connection just like we use to mention database connection in framework but here we mentions
// in program.cs file instead and we callin depedency injection since we inject depedant file
// wherever we want when we once mention it in program file
// now above we have access that connection using constructor of class ( type ctor )
// before that we have mentioned one private variable of ApplicationDbContext class we have created
// we are passing that connection to constructor


// 2->
// we have pass the data gathered from database to the view function which will display data
// on web page

//3-> 
// when we do some changes in page and we enter information on form which has post method we need
// to send back that information thats why we use same method with type [HttpPost]
// we have method add that will add the data to database and save changes will eep track of all 
// changes
// by using return to action we are goig back to index action
// we use ModelState.Isvalid to check if the model is valid and if all the validation mentions
// in attribute field of element in product model satisfies then it execute the statements