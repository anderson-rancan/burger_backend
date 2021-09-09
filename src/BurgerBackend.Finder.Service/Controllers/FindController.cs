using System;
using System.Collections.Generic;
using System.Linq;
using BurgerBackend.Finder.Service.ExternalServices;
using BurgerBackend.Finder.Service.ExternalServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BurgerFinderService.Controllers
{
    [ApiController]
    public sealed class FindController : ControllerBase
    {
        private readonly IBurgerShopService _burgerShopService;

        public FindController(IBurgerShopService burgerShopService)
        {
            _burgerShopService = burgerShopService;
        }

        [HttpGet("api/find/zip")]
        public IEnumerable<BurgerShop> FindByZip([FromQuery] IEnumerable<string> zip, [FromQuery] int? top, [FromQuery] int? skip)
        {
            if (zip?.Any() != true)
            {
                throw new BadHttpRequestException("Missing zip code argument list.");
            }

            return _burgerShopService.GetShopListByZipCode(zip, top.GetValueOrDefault(20), skip.GetValueOrDefault());
        }

        [HttpGet("api/find")]
        public IEnumerable<BurgerShop> GetAll([FromQuery] int? top, [FromQuery] int? skip)
            => _burgerShopService.GetAll(top.GetValueOrDefault(20), skip.GetValueOrDefault());

        [HttpGet("api/find/{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _burgerShopService.GetById(id);

            return result != null
                ? Ok(result)
                :  NotFound();
        }
    }
}
