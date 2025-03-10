using Eventopia.Data;
using Eventopia.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eventopia.Controllers;

public class TicketController : Controller
{
    
    private readonly EventopiaDbContext _context;

    public TicketController(EventopiaDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("/createTicket")]
    public IActionResult TicketCreate()
    {
        return View();
    }

    [HttpPost("/createTicket")]
    public async Task<IActionResult> TicketCreate(Ticket ticket)
    {
        if (ModelState.IsValid)
        {
            await _context.Tickets.AddAsync(ticket);
            return Redirect("/createEvent");
        }
        return View(ticket);
    }
}