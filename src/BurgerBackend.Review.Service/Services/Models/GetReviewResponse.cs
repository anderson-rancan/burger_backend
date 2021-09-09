using System;

namespace BurgerBackend.Review.Service.Services.Models
{
    public sealed class GetReviewResponse
    {
        public Guid Id { get; set; }
        public Guid VenueId { get; set; }
        public int TasteSctore { get; set; }
        public int TextureScore { get; set; }
        public int VisualScore { get; set; }
    }
}
