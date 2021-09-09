using System;
using BurgerBackend.Identity.Interface;
using BurgerBackend.Identity.Interface.Services.Models;
using BurgerBackend.Identity.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(Endpoints.Account.Authenticate)]
        public IActionResult Authenticate([FromBody] AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);
            return Ok(response);
        }

        [HttpPost(Endpoints.Account.ValidateToken)]
        public IActionResult ValidateToken([FromBody] string token)
        {
            var result = _userService.ValidateToken(token);

            return result != null
                ? Ok(result)
                : BadRequest("Token is not valid.");
        }

        [HttpPost(Endpoints.Account.Register)]
        public IActionResult Register([FromBody] RegisterRequest model)
        {
            _userService.Register(model);
            return Ok(new { message = "Registration successful" });
        }

        [HttpGet(Endpoints.Account.GetById)]
        public IActionResult GetById(Guid id)
        {
            var user = _userService.GetById(id);
            return Ok(user);
        }

        [HttpPut(Endpoints.Account.Update)]
        public IActionResult Update(Guid id, UpdateRequest model)
        {
            _userService.Update(id, model);
            return Ok(new { message = "User updated successfully" });
        }

        [HttpDelete(Endpoints.Account.Delete)]
        public IActionResult Delete(Guid id)
        {
            _userService.Delete(id);
            return Ok(new { message = "User deleted successfully" });
        }
    }
}
