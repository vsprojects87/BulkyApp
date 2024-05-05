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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            // 2
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll()
                .Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                });
            ProductVM productVM = new()
            // same as ProductVM productVM = new ProductVM
            {
                CategoryList = CategoryList,
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
        }
        // to the ProductVM we are passing values from product model and values for 
        // ddl from Categorylist which is getting data from categorymodel dbset which will
        // populate the dropdownlist for create

        // IEnumerable<SelectListItem> is used to represent a list of selectable items
        // for a dropdown list in a web application, commonly used in ASP.NET MVC or
        // ASP.NET Core applications.



        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            //3
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    // on update to delete the existing file from folder to avoid multiple files
                    // we are trimming start of imgurl since it contains \ and in wwwRootPath we already
                    // have \ at the end so it will have double \\ and it wont be able to find that location file 
                    if (!string.IsNullOrEmpty(productVM.Product.ImgUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath,productVM.Product.ImgUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath)) {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // here we are creating file on productPath location thats why in productPath we have
                    // address like wwwRootPath and combining with images\product which is path of current environment
                    // so we are basically storing the image on spefifies location with 'filename'
                    using (var fileStream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImgUrl = @"\images\product\" + filename;
                    // here we are using path like \images\product\ since we are creating
                    // string name to store in imgurl and not saving file
                    // in web host we were creating image file but with the imgurl
                    // we are only redirecting to local image\product folder
                }

                if (productVM.Product.Id == 0)
                {
					_unitOfWork.Product.Add(productVM.Product);
                    // if id is not present means we are creating
				}
				else
                {
					_unitOfWork.Product.Update(productVM.Product);
                    // if id found means we are updating
				}
				_unitOfWork.Save();
                TempData["Success"] = "Product Created Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll()
                .Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                });
                return View(productVM);

            }
        }
        // upsert- update and insert(create)


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