using System;
using System.Collections.Generic;
using BurgerBackend.Identity.Interface.Services.Models;
using BurgerBackend.Review.Service.Repositories;
using BurgerBackend.Review.Service.Services.Models;

namespace BurgerBackend.Review.Service.Services
{
    public interface IReviewService
    {
        Guid AddReview(Guid venueId, AddReviewRequest request, GetUserResponse user);
        GetReviewResponse GetReview(Guid venueId, Guid id);
        bool UpdateReview(Guid venueId, Guid id, UpdateReviewRequest request, GetUserResponse user);
        bool DeleteReview(Guid venueId, Guid id);
        IEnumerable<VenueReview> GetReviews(Guid venueId, int top, int skip);
    }

    internal sealed class ReviewService : IReviewService
    {
        private readonly IReviewInMemoryRepository _reviewInMemoryRepository;

        public ReviewService(IReviewInMemoryRepository reviewInMemoryRepository)
        {
            _reviewInMemoryRepository = reviewInMemoryRepository;
        }

        public Guid AddReview(Guid venueId, AddReviewRequest request, GetUserResponse user)
        {
            var id = Guid.NewGuid();

            _reviewInMemoryRepository.Add(new VenueReview
            {
                Id = id,
                VenueId = venueId,
                TasteSctore = request.TasteSctore,
                TextureScore = request.TextureScore,
                VisualScore = request.VisualScore,
                CreatedUserId = user.Id,
                LastUpdatedUserId = user.Id
            });

            return id;
        }

        public bool DeleteReview(Guid venueId, Guid id) => _reviewInMemoryRepository.Delete(venueId, id);

        public GetReviewResponse GetReview(Guid venueId, Guid id)
        {
            var review = _reviewInMemoryRepository.Get(venueId, id);

            return new GetReviewResponse
            {
                TasteSctore = review.TasteSctore,
                TextureScore = review.TextureScore,
                VisualScore = review.VisualScore
            };
        }

        public IEnumerable<VenueReview> GetReviews(Guid venueId, int top, int skip) => _reviewInMemoryRepository.GetAll(venueId, top, skip);

        public bool UpdateReview(Guid venueId, Guid id, UpdateReviewRequest request, GetUserResponse user)
        {
            var review = _reviewInMemoryRepository.Get(venueId, id);
            if (review == null) return false;

            review.TasteSctore = request.TasteSctore;
            review.TextureScore = request.TextureScore;
            review.VisualScore = request.VisualScore;
            review.LastUpdatedUserId = user.Id;

            return true;
        }
    }
}
