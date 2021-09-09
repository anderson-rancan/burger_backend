using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using BurgerBackend.Review.Service.Services.Models;

namespace BurgerBackend.Review.Service.Repositories
{
    public class ReviewInMemoryRepository : IReviewInMemoryRepository
    {
        private readonly ConcurrentDictionary<Guid, VenueReview> _venueReviews = new();

        public bool Add(VenueReview venueReview) => _venueReviews.TryAdd(venueReview.Id, venueReview);

        public VenueReview Get(Guid id) => _venueReviews.TryGetValue(id, out var review) ? review : null;

        public bool Delete(Guid id) => _venueReviews.TryRemove(id, out _);

        public IEnumerable<VenueReview> GetAll(Guid venueId, int top, int skip) => _venueReviews
            .Where(r => r.Value.VenueId == venueId)
            .OrderBy(r => r.Key)
            .Select(r => r.Value)
            .Skip(skip)
            .Take(top);
    }
}
