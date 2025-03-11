using Eventopia.Data;
using Eventopia.Models;
using Eventopia.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventopia.Controllers;

public class EventController : Controller
{
    private readonly EventService _eventService;

    public EventController(EventService eventService)
    {
        _eventService = eventService;
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
        if (ModelState.IsValid)
        {
            await _eventService.CreateEvent(model, date, time);
            return Redirect("/events");
        }
        return View(model);
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