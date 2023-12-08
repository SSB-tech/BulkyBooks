using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCProject.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        //private readonly ApplicationDbContext _db;
        //public CategoryController(ApplicationDbContext db)
        //{
        //    _db = db;
        //} 
        //before using generic repository

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult ListAll()
        {
            var data = _unitOfWork.Product.GetAll();
            return View(data);
        }
        //public IActionResult Create()
        //{
        //    return View();
        //}

        public IActionResult Upsert(int id)
        {
            //Product product = new();
            //IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
            //    u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString()
            //    }
            //    );

            //IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
            //    u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString()
            //    }
            //    );

            //Alternate to above SelectListItem

            ProductVM productVM = new ProductVM()
            {
                Product = new(),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
                   u => new SelectListItem
                   {
                       Text = u.Name,
                       Value = u.Id.ToString(),
                   }
                    ),
                CategoryList = _unitOfWork.Category.GetAll().Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }
                    )
            };

            if (id == 0)
            {
                //create
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.Product.FindFirstOrDefault(u=> u.Id == id);
                return View(productVM);
            }


        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Product model)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Product.Add(model);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Data Successfully Added";
        //        return RedirectToAction("ListAll");
        //    }
        //    return View(model);
        //}
        //public IActionResult Delete(int id)
        //{
        //    return ();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(_hostEnvironment.WebRootPath, @"images\product");
                    var extension = Path.GetExtension(file.FileName);
                    if (model.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, model.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    model.Product.ImageUrl = @"\images\product\" + fileName + extension;

                }
                if(model.Product.Id==0)
                {
                    _unitOfWork.Product.Add(model.Product);
                }
                _unitOfWork.Product.Update(model.Product);
                //_unitOfWork.Product.Update(model);
                _unitOfWork.Save();
                TempData["success"] = "Data Successfully Added";
                return RedirectToAction("ListAll");
            }
            return View(model);
        }


        #region APICALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _unitOfWork.Product.GetAll(includedProperties:"Category"); //if we needed covertype then we could modify the code as var result = _unitOfWork.Product.GetAll(includedProperties:"category,covertype")
            return Json(new { sushant = result});
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.Product.FindFirstOrDefault(u=>u.Id==id); //if we needed covertype then we could modify the code as var result = _unitOfWork.Product.GetAll(includedProperties:"category,covertype")
            if (obj == null)
            {
                return Json(new {success = false, message = "Error While Deleting."});
            }
            if (obj.ImageUrl != null)
            {
                var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            _unitOfWork.Product.Delete(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Successfully Deleted." });
        }

        #endregion

    }
}
