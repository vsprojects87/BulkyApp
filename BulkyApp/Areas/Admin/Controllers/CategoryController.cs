﻿using BulkyApp.Models;
using BulkyApp.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using BulkyApp.DataAccess.Repository.IRepository;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using BulkyApp.Utility;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    // we are telling using identity roles that only admin can access all the action methods
    public class CategoryController : Controller
    {
        // 1
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            // 2
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order Cannot be same as Name");
                // when we use this method we can throw custom error messages
            }
            //3
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category Created Successfully";
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
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.CategoryId == id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category Updated Successfully";
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
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.CategoryId == id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(u => u.CategoryId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Delete(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");

        }

    }
}
// First add the folder named 'Category' and add view in folder which is by default index.cshtml
// after adding view add the controller in controllers folder , add controller of same name as of view
// like for 'category' view add categorycontroller.cs controller, there must be controller name at the last

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
// in attribute field of element in category model satisfies then it execute the statements