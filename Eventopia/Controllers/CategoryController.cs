using Eventopia.Models;
using Eventopia.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Eventopia.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet("/category/create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("/category/create")]
    public async Task<IActionResult> Create(Category category)
    {

        await _categoryService.CreateCategory(category);
        return RedirectToAction("Index", "Home");
    }
}