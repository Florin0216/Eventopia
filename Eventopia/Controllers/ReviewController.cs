using System.Security.Claims;
using Eventopia.Models;
using Eventopia.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventopia.Controllers;

public class ReviewController : Controller
{
    private readonly IReviewService _reviewService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEventService _eventService;

    public ReviewController(IReviewService reviewService, IHttpContextAccessor httpContextAccessor, IEventService eventService)
    {
        _reviewService = reviewService;
        _httpContextAccessor = httpContextAccessor;
        _eventService = eventService;
    }

    [HttpGet("/{id:int}/review")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Create(int id)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        bool hasReviewed = await _reviewService.HasUserReviewedEvent(userId, id);
        
        if (hasReviewed)
        {
            return RedirectToAction("Dashboard", "User", new { id }); 
        }
        
        return View();
    }
    
    [HttpPost("/{id:int}/review")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Create(int id,Review review,int Rating)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var thisEvent = await _eventService.GetEventById(id);
        
        await _reviewService.CreateReview(review, userId, thisEvent.Id, Rating);
        return RedirectToAction("Dashboard", "User");
    }
}