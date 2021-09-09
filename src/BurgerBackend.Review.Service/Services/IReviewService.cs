using System;
using System.Collections.Generic;
using BurgerBackend.Identity.Interface.Services.Models;
using BurgerBackend.Review.Service.Services.Models;

namespace BurgerBackend.Review.Service.Services
{
    public interface IReviewService
    {
        Guid AddReview(Guid venueId, AddReviewRequest request, GetUserResponse user);
        GetReviewResponse GetReview(Guid id);
        bool UpdateReview(Guid id, UpdateReviewRequest request, GetUserResponse user);
        bool DeleteReview(Guid id);
        IEnumerable<VenueReview> GetReviews(Guid venueId, int top, int skip);
    }
}
