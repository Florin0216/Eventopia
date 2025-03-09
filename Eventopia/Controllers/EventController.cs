using Microsoft.AspNetCore.Mvc;

namespace Eventopia.Controllers;

public class EventController : Controller
{

    [HttpGet("/events")]
    public IActionResult EventListing()
    {
        return View();
    }

    public IActionResult EventDetails()
    {
        return View();
    }

    public IActionResult EventCreate()
    {
        return View();
    }

    public IActionResult Checkout()
    {
        return View();
    }

    public IActionResult Confirmation()
    {
        return View();
    }
}