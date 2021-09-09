using System;
using BurgerBackend.Identity.Interface.Services.Models;

namespace BurgerBackend.Identity.Service.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest request);
        Guid? ValidateToken(string token);
        GetUserResponse GetById(Guid id);
        void Register(RegisterRequest request);
        void Update(Guid id, UpdateRequest request);
        void Delete(Guid id);
    }
}
