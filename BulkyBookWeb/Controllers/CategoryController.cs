using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess1;
using BulkyBook.Models1;
using BulkyBook.Utility1;
using BulkyBook.DataAccess1.Repository.IRepository;

namespace BulkyBookWeb.Controllers;
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        //recupero le info dal db
        IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
        return View(objCategoryList);
    }

    //get create
    public IActionResult Create()
    {
        return View();
    }

    //creo la post
    [HttpPost] //serve perchè di default è una get
    [ValidateAntiForgeryToken] //evitare che l'utente la crei dall'url
    //modelstate -> controlla se i dati sono coerenti al model che ho fatto
    public IActionResult Create(Category category)
    {
        if(ModelState.IsValid)
        {
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        return View(category);
    }

    //GET
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        //var categoryFromDb = _db.Categories.Find(id);
        var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        if (categoryFromDbFirst == null)
        {
            return NotFound();
        }
        return View(categoryFromDbFirst);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
           ModelState.AddModelError(nameof(obj.Name), $"The name of property {nameof(obj.DisplayOrder)} cannot exactly match the name of property {nameof(obj.Name)}");
        }
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category updated successfully";
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
        //var categoryFromDb = _db.Categories.Find(id);
        var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        if (categoryFromDbFirst == null)
        {
            return NotFound();
        }
        return View(categoryFromDbFirst);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int id, [Bind("Id")] Category category)
    {
        if (id != category.Id)
        {
            return NotFound();
        }
        //var obj = _db.Categories.Find(id);
        var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        if (categoryFromDbFirst == null)
        {
            return NotFound();
        }
        _unitOfWork.Category.Remove(categoryFromDbFirst);
        _unitOfWork.Save();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction(nameof(Index));
    }
}