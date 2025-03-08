using Microsoft.AspNetCore.Mvc;

namespace Eventopia.Controllers;

public class UserController : Controller
{
    public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult OrganizerDash()
    {
        return View();
    }
}