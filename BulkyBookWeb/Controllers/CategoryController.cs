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
}