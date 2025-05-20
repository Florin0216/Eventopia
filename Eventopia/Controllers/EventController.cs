using System.Globalization;
using System.Security.Claims;
using Eventopia.Models;
using Eventopia.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Eventopia.Controllers;

public class EventController : Controller
{
    private readonly IEventService _eventService;
    private readonly ICategoryService _categoryService;
    private readonly IVenueService _venueService;
    private readonly ITicketService _ticketService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICheckoutService _checkoutService;

    public EventController(IEventService eventService, 
        IHttpContextAccessor httpContextAccessor, 
        ICategoryService categoryService, 
        IVenueService venueService, 
        ITicketService ticketService,
        ICheckoutService checkoutService)
    {
        _eventService = eventService;
        _httpContextAccessor = httpContextAccessor;
        _categoryService = categoryService;
        _venueService = venueService;
        _ticketService = ticketService;
        _checkoutService = checkoutService;
    }
    
    [HttpGet("/events")]
    public async Task<ViewResult> EventListing(string category = "all", string date = "all", string location = "all", string search = "")
    {
        var categories = await _categoryService.GetAllCategories();
        var events = await _eventService.GetAllEvents();
        var locations = await _venueService.GetAllVenues();

        if (!string.IsNullOrEmpty(search))
        {
            events = events.Where(e => e.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        
        if (category != "all")
        {
           events = await _categoryService.GetEventsByCategoryAsync(category);
        }

        if (date != "all")
        {
            events = await _eventService.FilterEventsByDateAsync(events, date);
        }
        
        if (location != "all")
        {
            events = await _venueService.GetEventsByVenueAsync(location);
        }
        
        var priceRanges = await _eventService.GetEventsPriceRangesAsync(events);
        
        ViewBag.PriceRanges = priceRanges;
        ViewBag.Categories = categories;
        ViewBag.Venues = locations;
        ViewBag.SelectedVenue = location;
        ViewBag.SelectedCategory = category;
        ViewBag.SelectedDate = date;
        ViewBag.SearchTerm = search;
    
        return View(events);
    }

    [HttpGet("/event/{id:int}/details")] 
    public async Task<IActionResult> EventDetails(int id)
    {
        var thisEvent = await _eventService.GetEventById(id);
        if (thisEvent == null)
        {
            return NotFound();
        }
        return View(thisEvent);
    }
    
    [HttpGet("/createEvent")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> EventCreate()
    {
        ViewBag.Categories = await _categoryService.GetAllCategories();
        ViewBag.Venues = await _venueService.GetAllVenues();
        return View();
    }
    
    [HttpPost("/createEvent")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> EventCreate(Event model, string date, string time, IFormFile Photo)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (Photo != null && Photo.Length > 0)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "EventImages");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Photo.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Photo.CopyToAsync(stream);
            }

            model.PhotoPath = $"~/Images/EventImages/{fileName}";
        }

        var eventId = await _eventService.CreateEvent(model, date, time, userId);
        return RedirectToAction("TicketCreate", "Ticket", new { eventId });
    }

    [HttpGet("/event/{id:int}/update")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> EventUpdate(int id)
    {
        var eventItem = await _eventService.GetEventById(id);
        return View(eventItem);
    }

    [HttpPost("/event/{id:int}/update")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> EventUpdate(int id,Event model, string date, string time)
    {
        if (!ModelState.IsValid) return View(model);
        await _eventService.UpdateEvent(id,model, date, time);
        return RedirectToAction("OrganizerDash", "User");
    }
    
    [HttpGet("/event/{id:int}/delete")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> EventDelete(int id)
    {
        var eventItem = await _eventService.GetEventById(id);
        await _eventService.DeleteEvent(eventItem);
        return RedirectToAction("OrganizerDash", "User");
    }

    [HttpGet("/event/{id:int}/checkout")]
    [Authorize(Roles = "Organizer,User")]
    public async Task<IActionResult> Checkout(int id, string ticketType, int quantity)
    {
        var tickets =await _ticketService.GetTicketsForEvent(id);
        var ticket = tickets.FirstOrDefault(t => t.Type == ticketType);
        if (ticket == null) return View();
        var totalAmount = ticket.Price * quantity;
        
        ViewBag.TicketType = ticketType;
        ViewBag.Quantity = quantity;
        ViewBag.TotalAmount = totalAmount;

        return View();
    }
    
    [HttpPost("/event/{eventId:int}/checkout")]
    [Authorize(Roles = "Organizer,User")]
    public async Task<IActionResult> Checkout(int eventId, Checkout checkout)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var eventItem = await _eventService.GetEventById(eventId);
        var tickets = await _ticketService.GetTicketsForEvent(eventId);
        var ticket = tickets.FirstOrDefault(t => t.Type == checkout.Type);
        if (ticket != null) ticket.Quantity -= checkout.Quantity;

        if (ticket != null) await _ticketService.UpdateTicket(ticket);

        var thisCheckout = await _checkoutService.CreateCheckout(checkout, eventId, eventItem?.Venue?.Name, userId);

        return RedirectToAction("Confirmation", "Event",new { checkoutId = thisCheckout.Id });
    }
    

    [HttpGet("/{checkoutId:int}/confirmation")]
    [Authorize(Roles = "Organizer,User")]
    public async Task<IActionResult> Confirmation(int checkoutId)
    {
        var checkout = await _checkoutService.FindCheckoutById(checkoutId);
        return View(checkout);
    }
}