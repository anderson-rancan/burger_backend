using System;
using System.Collections.Generic;
using BurgerBackend.Review.Service.Services.Models;

namespace BurgerBackend.Review.Service.Repositories
{
    public interface IReviewInMemoryRepository
    {
        bool Add(VenueReview venueReview);
        VenueReview Get(Guid id);
        bool Delete(Guid id);
        IEnumerable<VenueReview> GetAll(Guid venueId, int top, int skip);
    }
}
