using System.Security.Claims;
using Eventopia.Data;
using Eventopia.Models;
using Eventopia.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventopia.Controllers;

public class EventController : Controller
{
    private readonly EventService _eventService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EventController(EventService eventService, IHttpContextAccessor httpContextAccessor)
    {
        _eventService = eventService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet("/events")]
    public async Task<ViewResult> EventListing()
    {
        var events = await _eventService.GetAllEvents();
        return View(events);
    }

    public IActionResult EventDetails()
    {

        return View();
    }
    
    [HttpGet("/createEvent")]
    [Authorize(Roles = "Organizer")]
    public IActionResult EventCreate()
    {
        return View();
    }
    
    [HttpPost("/createEvent")]
    public async Task<IActionResult> EventCreate(Event model,string date, string time)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (ModelState.IsValid)
        {
            var eventId = await _eventService.Create(model, date, time, userId);
            return RedirectToAction("TicketCreate", "Ticket", new { eventId = eventId });
        }
        return View(model);
    }

    [HttpGet("/updateEvent/{id}")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> EventUpdate(int id)
    {
        var eventItem = await _eventService.GetEventById(id);
        return View(eventItem);
    }

    [HttpPost("/updateEvent/{id}")]
    public async Task<IActionResult> EventUpdate(int id,Event model, string date, string time)
    {
        if (ModelState.IsValid)
        {
            await _eventService.Update(id,model, date, time);
            return RedirectToAction("OrganizerDash", "User");
        }
        return View(model);
    }
    
    [HttpGet("/deleteEvent/{id}")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> EventDelete(int id)
    {
        var eventItem = await _eventService.GetEventById(id);
        await _eventService.Delete(eventItem);
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