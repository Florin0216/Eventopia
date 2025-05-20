using Eventopia.Models;
using Eventopia.Repositories.Interfaces;
using Eventopia.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eventopia.Services;

public class ReviewService : IReviewService
{
    private readonly IRepositoryWrapper _repositoryWrapper;

    public ReviewService(IRepositoryWrapper repositoryWrapper)
    {
        _repositoryWrapper = repositoryWrapper;
    }

    public async Task CreateReview(Review review, string userId, int eventId, int Rating)
    {
        review.ReviewDate = DateTime.UtcNow;
        review.EventId = eventId;
        review.UserId = userId;
        review.ReviewerRating = Rating;
        
        _repositoryWrapper.Review.Create(review);
        await _repositoryWrapper.SaveAsync();
    }
    
    public async Task<bool> HasUserReviewedEvent(string userId, int eventId)
    {
        return await _repositoryWrapper.Review.FindByCondition(r => 
                r.UserId == userId && r.EventId == eventId)
            .AnyAsync();
    }
    
}