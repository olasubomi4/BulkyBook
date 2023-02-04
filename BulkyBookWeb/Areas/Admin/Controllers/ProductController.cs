using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Bulkybook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.JSInterop;

namespace BulkyBookWeb.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostEnvironment;

    public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment hostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _hostEnvironment = hostEnvironment;
    }
    // GET
    public IActionResult Index()
    {
        IEnumerable<Product> objProductList = _unitOfWork.Product.GetAll();
        return View(objProductList);
    }


    
   
    
    public IActionResult Upsert(int? id)
    {
        ProductVM productVM = new()
        {
            product = new Product(),
            CategoryList = _unitOfWork.Category.GetAll().Select(
                c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }
            ),
            CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
            c=> new SelectListItem
            { 
                Text =c.Name,
                Value = c.Id.ToString()
            }
        )
            }
            ;
        if (id == null || id == 0)
        {
            return View(productVM);
        }
        else
        {
            productVM.product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            return View(productVM);
        }
        
    
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductVM obj,IFormFile file)
    {
        if (!ModelState.IsValid)
        {
            return Redirect("index");
        }

        string wwwRootPath = _hostEnvironment.WebRootPath;
        if (file != null)
        {
            string fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(wwwRootPath, @"images/products");
            var extension = Path.GetExtension(file.FileName);

            if (obj.product.ImageUrl != null)
            {
                var oldImagePath = Path.Combine(wwwRootPath, obj.product.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                file.CopyTo(fileStreams);
            }

            obj.product.ImageUrl = @"/Images/products/" + fileName + extension;

        }

        if (obj.product.Id == 0)
        {
            _unitOfWork.Product.Add(obj.product);
        }
        else
        {
            _unitOfWork.Product.Update(obj.product);
        }
    
        _unitOfWork.Save();
        if (obj.product.Id == 0)
        {
            TempData["success"] = "Product created successfully";
        }
        else
        {
            TempData["success"] = "Product Updated successfully";
        }

        return RedirectToAction("Index");
    }
   /* public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var CoverTypeFromDb = _unitOfWork.Product.GetFirstOrDefault(x=>x.Id==id);
        
        return View(CoverTypeFromDb);
    }
    */
    
  

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
        return Json(new { data = productList });
    }
    
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var obj = _unitOfWork.Product.GetFirstOrDefault(x=>x.Id==id);
        if (obj == null)
        {
            return Json(new { success = false, Message = "Error while deleting" });
        }
        var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('/'));
        if (System.IO.File.Exists(oldImagePath))
        {
            System.IO.File.Delete(oldImagePath);
        }
        _unitOfWork.Product.Remove(obj);
        _unitOfWork.Save();
        return Json(new { success = true, Message = "Delete successful" });
        
    }

    #endregion

    
}
