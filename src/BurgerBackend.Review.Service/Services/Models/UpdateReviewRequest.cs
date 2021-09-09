namespace BurgerBackend.Review.Service.Services.Models
{
    public sealed class UpdateReviewRequest
    {
        public int TasteSctore { get; set; }
        public int TextureScore { get; set; }
        public int VisualScore { get; set; }
    }
}
