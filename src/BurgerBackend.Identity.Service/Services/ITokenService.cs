using System;
using IdentityService.Domain;

namespace BurgerBackend.Identity.Service.Services
{
    internal interface ITokenService
    {
        string GenerateToken(ApplicationUser user);
        Guid? ValidateToken(string token);
    }
}
