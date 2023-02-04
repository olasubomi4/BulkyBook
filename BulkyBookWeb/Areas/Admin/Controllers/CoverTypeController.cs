using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Bulkybook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CoverTypeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CoverTypeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    // GET
    public IActionResult Index()
    {
        IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
        
        return View(objCoverTypeList);
    }



    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CoverType obj)
    {
       
        if (!ModelState.IsValid)
        {
            return View(obj);
        }

        _unitOfWork.CoverType.Add(obj);
        _unitOfWork.Save();
        TempData["success"] = "CoverType created successfully";
        return RedirectToAction("Index");
    }
    
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var CoverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(x=>x.Id==id);
        
        return View(CoverTypeFromDb);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType obj)
    {
     
        if (!ModelState.IsValid)
        {
            return View(obj);
        }
        _unitOfWork.CoverType.Update(obj);
        _unitOfWork.Save();
        TempData["success"] = "CoverType edited successfully";
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var CoverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(x=>x.Id==id);
        
        return View(CoverTypeFromDb);
    }
    
    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        var obj = _unitOfWork.CoverType.GetFirstOrDefault(x=>x.Id==id);
        if (obj == null)
        {
            return NotFound();
        }
        _unitOfWork.CoverType.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "CoverType deleted successfully";
        return RedirectToAction("Index");
    }
}
