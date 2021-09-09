using System;

namespace BurgerBackend.Review.Service.Services.Models
{
    public sealed class VenueReview
    {
        public Guid Id { get; set; }
        public Guid VenueId { get; set; }
        public int TasteSctore { get; set; }
        public int TextureScore { get; set; }
        public int VisualScore { get; set; }
        public Guid CreatedUserId { get; set; }
        public Guid LastUpdatedUserId { get; set; }
    }
}
