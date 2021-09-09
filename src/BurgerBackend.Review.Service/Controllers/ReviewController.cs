using System;
using BurgerBackend.Identity.Interface.Services.Models;
using BurgerBackend.Review.Service.Services;
using BurgerBackend.Review.Service.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Review.Service.Controllers
{
    [ApiController]
    public sealed class ReviewController : ControllerBase
    {
        public readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("api/venue/{venueId}/review")]
        public IActionResult AddReview(Guid venueId, AddReviewRequest request)
        {
            var user = GetUser();
            if (user == null) return Unauthorized();

            if (request.TasteSctore is < 0 or > 5) return BadRequest($"{ nameof(request.TasteSctore) } value should be between 0 and 5.");
            if (request.TextureScore is < 0 or > 5) return BadRequest($"{ nameof(request.TextureScore) } value should be between 0 and 5.");
            if (request.VisualScore is < 0 or > 5) return BadRequest($"{ nameof(request.VisualScore) } value should be between 0 and 5.");

            return Ok(_reviewService.AddReview(venueId, request, user));
        }

        [HttpGet("api/venue/review/{id}")]
        public IActionResult GetReview(Guid id)
        {
            var review = _reviewService.GetReview(id);
            return review != null ? Ok(review) : NotFound();
        }

        [HttpPut("api/venue/review/{id}")]
        public IActionResult UpdateReview(Guid id, UpdateReviewRequest request)
        {
            var user = GetUser();
            if (user == null) return Unauthorized();

            if (request.TasteSctore is < 0 or > 5) return BadRequest($"{ nameof(request.TasteSctore) } value should be between 0 and 5.");
            if (request.TextureScore is < 0 or > 5) return BadRequest($"{ nameof(request.TextureScore) } value should be between 0 and 5.");
            if (request.VisualScore is < 0 or > 5) return BadRequest($"{ nameof(request.VisualScore) } value should be between 0 and 5.");

            return _reviewService.UpdateReview(id, request, user) ? Ok() : NotFound();
        }

        [HttpDelete("api/venue/review/{id}")]
        public IActionResult DeleteReview(Guid id)
            => _reviewService.DeleteReview(id) ? Ok() : NotFound();

        [HttpGet("api/venue/{venueId}/reviews")]
        public IActionResult GetReviews(Guid venueId, [FromQuery] int? top, [FromQuery] int? skip)
            => Ok(_reviewService.GetReviews(venueId, top.GetValueOrDefault(20), skip.GetValueOrDefault()));

        [HttpPost("api/venue/{venueId}/picture")]
        public IActionResult AddPicture(Guid venueId, IFormFile file)
        {
            throw new NotImplementedException();
        }

        [HttpPost("api/venue/{venueId}/pictures")]
        public IActionResult GetPictures(Guid venueId, [FromQuery] int? top, [FromQuery] int? skip)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("api/venue/picture/{id}")]
        public IActionResult DeletePicture(Guid id)
        {
            throw new NotImplementedException();
        }

        private GetUserResponse GetUser()
        {
            var hasUser = HttpContext.Items.TryGetValue("User", out var userObject);
            if (!hasUser) return null;

            return userObject as GetUserResponse;
        }
    }
}
