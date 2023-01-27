using Microsoft.AspNetCore.Mvc;
using BulkyBookWeb.Data;
using BulkyBookWeb.Models;

namespace BulkyBookWeb.Controllers;
public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult Index()
    {
        //recupero le info dal db
        IEnumerable<Category> objCategoryList = _db.Categories;
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
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(category);
    }
}