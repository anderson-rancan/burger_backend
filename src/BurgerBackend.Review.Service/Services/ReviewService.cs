using System;
using System.Collections.Generic;
using BurgerBackend.Identity.Interface.Services.Models;
using BurgerBackend.Review.Service.Repositories;
using BurgerBackend.Review.Service.Services.Models;

namespace BurgerBackend.Review.Service.Services
{
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

        public bool DeleteReview(Guid id) => _reviewInMemoryRepository.Delete(id);

        public GetReviewResponse GetReview(Guid id)
        {
            var review = _reviewInMemoryRepository.Get(id);

            return new GetReviewResponse
            {
                Id = review.Id,
                VenueId = review.VenueId,
                TasteSctore = review.TasteSctore,
                TextureScore = review.TextureScore,
                VisualScore = review.VisualScore
            };
        }

        public IEnumerable<VenueReview> GetReviews(Guid venueId, int top, int skip) => _reviewInMemoryRepository.GetAll(venueId, top, skip);

        public bool UpdateReview(Guid id, UpdateReviewRequest request, GetUserResponse user)
        {
            var review = _reviewInMemoryRepository.Get(id);
            if (review == null) return false;

            review.TasteSctore = request.TasteSctore;
            review.TextureScore = request.TextureScore;
            review.VisualScore = request.VisualScore;
            review.LastUpdatedUserId = user.Id;

            return true;
        }
    }
}
