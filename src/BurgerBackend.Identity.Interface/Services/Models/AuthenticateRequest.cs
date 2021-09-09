using System.ComponentModel.DataAnnotations;

namespace BurgerBackend.Identity.Interface.Services.Models
{
    public sealed class AuthenticateRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
