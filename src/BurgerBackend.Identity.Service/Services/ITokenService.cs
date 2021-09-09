using System;
using BurgerBackend.Identity.Service.Services.Models;

namespace BurgerBackend.Identity.Service.Services
{
    internal interface ITokenService
    {
        string GenerateToken(ApplicationUser user);
        Guid? ValidateToken(string token);
    }
}
