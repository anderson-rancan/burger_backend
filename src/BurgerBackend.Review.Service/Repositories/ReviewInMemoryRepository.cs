using System;
using System.Collections.Generic;
using System.Linq;
using BurgerBackend.Review.Service.Services.Models;

namespace BurgerBackend.Review.Service.Repositories
{
    public interface IReviewInMemoryRepository
    {
        void Add(VenueReview venueReview);
        VenueReview Get(Guid venueId, Guid id);
        bool Delete(Guid venueId, Guid id);
        IEnumerable<VenueReview> GetAll(Guid venueId, int top, int skip);
    }


    public class ReviewInMemoryRepository : IReviewInMemoryRepository
    {
        private readonly List<VenueReview> _venueReviews = new();

        public void Add(VenueReview venueReview) => _venueReviews.Add(venueReview);

        public VenueReview Get(Guid venueId, Guid id) => _venueReviews.FirstOrDefault(r => r.VenueId == venueId && r.Id == id);

        public bool Delete(Guid venueId, Guid id)
        {
            var review = _venueReviews.FirstOrDefault(r => r.VenueId == venueId && r.Id == id);
            
            if (review != null)
            {
                _venueReviews.Remove(review);
                return true;
            }

            return false;
        }

        public IEnumerable<VenueReview> GetAll(Guid venueId, int top, int skip)
            => _venueReviews.Where(r => r.VenueId == venueId).OrderBy(r => r.Id).Skip(skip).Take(top);
    }
}
