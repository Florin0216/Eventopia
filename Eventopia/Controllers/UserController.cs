using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventopia.Controllers;

public class UserController : Controller
{
    [HttpGet]
    [Authorize(Policy = "OnlyForUser")]
    [Route("/user/dashboard")]
    public IActionResult Dashboard()
    {
        return View();
    }

    [HttpGet]
    [Authorize(Roles = "Organizer")]
    [Route("/organizer/dashboard")]
    public IActionResult OrganizerDash()
    {
        return View();
    }
}