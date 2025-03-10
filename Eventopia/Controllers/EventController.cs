using Eventopia.Data;
using Eventopia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventopia.Controllers;

public class EventController : Controller
{
    private readonly EventopiaDbContext _context;

    public EventController(EventopiaDbContext context)
    {
        _context = context;
    }

    [HttpGet("/events")]
    public IActionResult EventListing()
    {
        return View();
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
    public async Task<IActionResult> EventCreate(Event model, List<Ticket> tickets)
    {
        if (ModelState.IsValid)
        {
            _context.Events.Add(model);
            await _context.SaveChangesAsync();
            
            foreach (var ticket in tickets)
            {
                var eventTicket = new TicketEvent
                {
                    EventId = model.Id,
                    Ticket = ticket
                };
                _context.TicketEvents.Add(eventTicket);
            }
            await _context.SaveChangesAsync();

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