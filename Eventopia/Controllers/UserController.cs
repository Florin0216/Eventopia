using System.Security.Claims;
using Eventopia.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventopia.Controllers;

public class UserController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly EventService _eventService;

    public UserController(IHttpContextAccessor httpContextAccessor, EventService eventService)
    {
        _httpContextAccessor = httpContextAccessor;
        _eventService = eventService;
    }
    
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
    public async Task<IActionResult> OrganizerDash()
    {
        var events = await _eventService.GetEventsByOrganizer(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        return View(events);
    }
}