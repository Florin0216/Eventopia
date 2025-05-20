using System.Security.Claims;
using Eventopia.Models;
using Eventopia.Services;
using Eventopia.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eventopia.Controllers;

public class UserController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEventService _eventService;
    private readonly UserManager<Users> _userManager;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IUserService _userService;
    private readonly ICheckoutService _checkoutService;

    public UserController(
        IHttpContextAccessor httpContextAccessor,
        IEventService eventService,
        UserManager<Users> userManager,
        IWebHostEnvironment webHostEnvironment,
        IUserService userService,
        ICheckoutService checkoutService)
    {
        _httpContextAccessor = httpContextAccessor;
        _eventService = eventService;
        _userManager = userManager;
        _webHostEnvironment = webHostEnvironment;
        _userService = userService;
        _checkoutService = checkoutService;
    }

    [HttpGet]
    [Authorize(Roles = "User")]
    [Route("/user/dashboard")]
    public async Task<IActionResult> Dashboard(string status = "Upcoming")
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _userManager.FindByIdAsync(userId);
        var checkouts = await _checkoutService.FindCheckoutsByUserId(userId);


        checkouts = checkouts
            .Where(c => c.Event?.Status?.Equals(status, StringComparison.OrdinalIgnoreCase) == true)
            .ToList();


        ViewBag.ProfilePicture = string.IsNullOrEmpty(user.ProfilePicturePath)
            ? "~/Images/user.png"
            : user.ProfilePicturePath;

        return View(checkouts);
    }

    [HttpGet]
    [Authorize(Roles = "Organizer")]
    [Route("/organizer/dashboard")]
    public async Task<IActionResult> OrganizerDash()
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _userManager.FindByIdAsync(userId);

        ViewBag.ProfilePicture = string.IsNullOrEmpty(user.ProfilePicturePath)
            ? "~/Images/user.png"
            : user.ProfilePicturePath;

        var events = await _eventService.GetEventsByOrganizer(userId);

        decimal totalTicketsSold = 0;
        decimal totalRevenue = 0;
        int activeEventsCount = events.Count(e => e.Status.Equals("Upcoming", StringComparison.OrdinalIgnoreCase));

        var ticketsSoldPerEvent = new Dictionary<int, int>();

        foreach (var ev in events)
        {
            var checkouts = await _checkoutService.FindCheckoutsByEventId(ev.Id);
            int eventTicketsSold = 0;

            foreach (var checkout in checkouts)
            {
                totalRevenue += checkout.Amount;
                totalTicketsSold += checkout.Quantity;
                eventTicketsSold += checkout.Quantity;
            }

            ticketsSoldPerEvent[ev.Id] = eventTicketsSold;
        }

        ViewBag.TotalTicketsSold = totalTicketsSold;
        ViewBag.TotalRevenue = totalRevenue.ToString("C", new System.Globalization.CultureInfo("en-US"));
        ViewBag.ActiveEvents = activeEventsCount;
        ViewBag.TicketsSoldPerEvent = ticketsSoldPerEvent;

        return View(events);
    }


    [HttpPost]
    [Authorize(Roles = "Organizer,User")]
    [Route("/upload-profile")]
    public async Task<IActionResult> UploadProfilePicture(IFormFile profilePic)
    {
        if (profilePic != null && profilePic.Length > 0)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "ProfilePictures");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(profilePic.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await profilePic.CopyToAsync(stream);
            }

            if (user != null)
            {
                user.ProfilePicturePath = $"~/Images/ProfilePictures/{fileName}";
                await _userManager.UpdateAsync(user);
            }
        }

        if (User.IsInRole("Organizer"))
        {
            return RedirectToAction("OrganizerDash");
        }

        return RedirectToAction("Dashboard");
    }
}