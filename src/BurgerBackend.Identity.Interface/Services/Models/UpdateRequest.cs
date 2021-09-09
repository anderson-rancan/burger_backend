using System.ComponentModel.DataAnnotations;

namespace BurgerBackend.Identity.Interface.Services.Models
{
    public sealed class UpdateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
