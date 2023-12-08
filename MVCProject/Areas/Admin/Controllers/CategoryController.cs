using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVCProject.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        //private readonly ApplicationDbContext _db;
        //public CategoryController(ApplicationDbContext db)
        //{
        //    _db = db;
        //} 
        //before using generic repository

        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult ListAll()
        {
            var data = _unitOfWork.Category.GetAll();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var data = _unitOfWork.Category.FindFirstOrDefault(u => u.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);

        }
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var data = _unitOfWork.Category.FindFirstOrDefault(u => u.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);

            //_clientNotification.AddToastNotification("From Index of home",
            //                               NotificationType.success,
            //                               null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category model)
        {
            if (model.Name == model.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and DisplayOrder Cannot be Same."); //Here error will be displayed below name property because we have used name as the key i.e  ModelState.AddModelError("Key", "Value"). Also esma name vaneko model ko name ho esma random name halna mildaina.
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(model);
                _unitOfWork.Save();
                TempData["success"] = "Data Successfully Added";
                return RedirectToAction("ListAll");
            }
            return View(model);
        }
        //public IActionResult Delete(int id)
        //{
        //    return ();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category model)
        {
            if (model.Name == model.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name and DisplayOrder Cannot be Same."); //Here error will be displayed below name property because we have used name as the key i.e  ModelState.AddModelError("Key", "Value")
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(model);
                _unitOfWork.Save();
                TempData["success"] = "Data Successfully Edited";
                return RedirectToAction("ListAll");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category model)
        {
            if (model.Name == model.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name and DisplayOrder Cannot be Same."); //Here error will be displayed below name property because we have used name as the key i.e  ModelState.AddModelError("Key", "Value")
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Delete(model);
                _unitOfWork.Save();
                TempData["error"] = "Data Successfully Deleted";
                return RedirectToAction("ListAll");
            }
            return View(model);
        }

    }
}
