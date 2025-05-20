using Eventopia.Models;
using Eventopia.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventopia.Controllers;

public class VenueController : Controller
{
    private readonly IVenueService _venueService;

    public VenueController(IVenueService venueService)
    {
        _venueService = venueService;
    }
    
    [HttpGet("/venue/create")]
    [Authorize(Roles = "Organizer")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("/venue/create")]
    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> Create(Venue venue)
    {

        await _venueService.CreateVenue(venue);
        return RedirectToAction("Index", "Home");
    }
}