using Eventopia.Models;

namespace Eventopia.Services.Interfaces;

public interface IReviewService
{
    Task CreateReview(Review review, string userId, int eventId, int Rating);
    Task<bool> HasUserReviewedEvent(string userId, int eventId);
}