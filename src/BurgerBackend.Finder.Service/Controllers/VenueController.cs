using System;
using BurgerBackend.Finder.Service.ExternalServices;
using BurgerBackend.Finder.Service.ExternalServices.Models;
using BurgerBackend.Identity.Interface.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace BurgerBackend.Finder.Service.Controllers
{
    [ApiController]
    public sealed class VenueController : ControllerBase
    {
        private readonly IBurgerShopService _burgerShopService;

        public VenueController(IBurgerShopService burgerShopService)
        {
            _burgerShopService = burgerShopService;
        }

        [HttpPost("api/venue")]
        public IActionResult Add(AddVenueRequest request)
        {
            var user = GetUser();
            if (user == null) return Unauthorized();

            return Ok(_burgerShopService.Add(request, user));
        }

        [HttpGet("api/venue/{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _burgerShopService.GetById(id);

            return result != null
                ? Ok(result)
                : NotFound();
        }

        [HttpPut("api/venue/{id}")]
        public IActionResult AddOrUpdate(Guid id, UpdateVenueRequest request)
        {
            var user = GetUser();
            if (user == null) return Unauthorized();

            _burgerShopService.AddOrUpdate(id, request, user);
            return Ok();
        }

        [HttpDelete("api/venue/{id}")]
        public IActionResult Delete(Guid id) => _burgerShopService.Delete(id) ? Ok() : NotFound();

        private GetUserResponse GetUser()
        {
            var hasUser = HttpContext.Items.TryGetValue("User", out var userObject);
            if (!hasUser) return null;

            return userObject as GetUserResponse;
        }
    }
}
