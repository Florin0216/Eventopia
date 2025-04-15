using System.Security.Claims;
using Eventopia.Data;
using Eventopia.Models;
using Eventopia.Services;
using Eventopia.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventopia.Controllers;

public class EventController : Controller
{
    private readonly IEventService _eventService;
    private readonly ICategoryService _categoryService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EventController(IEventService eventService, IHttpContextAccessor httpContextAccessor, ICategoryService categoryService)
    {
        _eventService = eventService;
        _httpContextAccessor = httpContextAccessor;
        _categoryService = categoryService;
    }
    
    [HttpGet("/events")]
    public async Task<ViewResult> EventListing(string category = "all")
    {
        var categories = await _categoryService.GetAllCategories();
        var events = await _eventService.GetAllEvents();

        if (category != "all")
        {
           events = await _categoryService.GetEventsByCategoryAsync(category);
        }
        
        ViewBag.Categories = categories;
        ViewBag.SelectedCategory = category;
    
        return View(events);
    }

    public IActionResult EventDetails()
    {
        return View();
    }
    
    [HttpGet("/createEvent")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> EventCreate()
    {
        ViewBag.Categories = await _categoryService.GetAllCategories();
        return View();
    }
    
    [HttpPost("/createEvent")]
    public async Task<IActionResult> EventCreate(Event model,string date, string time)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!ModelState.IsValid) return View(model);
        var eventId = await _eventService.CreateEvent(model, date, time, userId);
        return RedirectToAction("TicketCreate", "Ticket", new { eventId = eventId });
    }

    [HttpGet("/updateEvent/{id:int}")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> EventUpdate(int id)
    {
        var eventItem = await _eventService.GetEventById(id);
        return View(eventItem);
    }

    [HttpPost("/updateEvent/{id:int}")]
    public async Task<IActionResult> EventUpdate(int id,Event model, string date, string time)
    {
        if (!ModelState.IsValid) return View(model);
        await _eventService.UpdateEvent(id,model, date, time);
        return RedirectToAction("OrganizerDash", "User");
    }
    
    [HttpGet("/deleteEvent/{id:int}")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> EventDelete(int id)
    {
        var eventItem = await _eventService.GetEventById(id);
        await _eventService.DeleteEvent(eventItem);
        return RedirectToAction("OrganizerDash", "User");
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