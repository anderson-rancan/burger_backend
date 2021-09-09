using System;

namespace BurgerBackend.Identity.Interface.Services.Models
{
    public sealed class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string JwtToken { get; set; }
    }
}
