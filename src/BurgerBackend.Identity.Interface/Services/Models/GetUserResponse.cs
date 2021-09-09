using System;

namespace BurgerBackend.Identity.Interface.Services.Models
{
    public sealed class GetUserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
