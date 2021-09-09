using System;
using IdentityService.Domain;

namespace IdentityService.Repositories
{
    internal interface IUserInMemoryRespository
    {
        ApplicationUser GetUserByEmail(string email);
        ApplicationUser GetUserById(Guid id);
        void SaveOrUpdateUser(ApplicationUser user);
        void DeleteUser(Guid id);
    }
}
