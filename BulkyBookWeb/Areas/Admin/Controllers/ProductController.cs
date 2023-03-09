using System;
using BulkyBook.DataAccess1;
using BulkyBook.DataAccess1.Repository.IRepository;
using BulkyBook.Models1;
using BulkyBook.Models1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Controllers;

[Area("Admin")]
public class ProductController:Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    //GET
    public IActionResult Index()
    {
        IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
        return View(objCoverTypeList);
    }

    //GET
    public IActionResult Upsert(int? id)
    {
        ProductVM productVM = new() {
            Product = new Product(),
            CategoryList = _unitOfWork.Category.GetAll().Select(
            u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }),
        CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
            u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }),
        };
        
        if (id == null || id == 0)
        {
            //create product
            return View(productVM);
        }
        else
        {
            //update product
        }
        return View(productVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Product obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Product.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product updated successfully";
            return RedirectToAction(nameof(Index));
        }
        return View(obj);
    }

    //GET
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var productFromDbFirst = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
        if (productFromDbFirst == null)
        {
            return NotFound();
        }
        return View(productFromDbFirst);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int id, [Bind("Id")] Product product)
    {
        if (id != product.Id)
        {
            return NotFound();
        }
        var productFromDbFirst = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
        if (productFromDbFirst == null)
        {
            return NotFound();
        }
        _unitOfWork.Product.Remove(productFromDbFirst);
        _unitOfWork.Save();
        TempData["success"] = "Product deleted successfully";
        return RedirectToAction(nameof(Index));
    }
}