using System;
using System.Collections.Concurrent;
using System.Linq;
using BurgerBackend.Identity.Service.Services.Models;

namespace IdentityService.Repositories
{
    // TODO this should be a real repository
    internal sealed class UserInMemoryRespository : IUserInMemoryRespository
    {
        private readonly ConcurrentDictionary<string, ApplicationUser> _inMemoryRepository = new();

        public ApplicationUser GetUserByEmail(string email) => _inMemoryRepository.TryGetValue(email, out var user) ? user : null;

        public ApplicationUser GetUserById(Guid id)
        {
            var user = _inMemoryRepository.FirstOrDefault(u => u.Value.Id == id);
            return string.IsNullOrWhiteSpace(user.Key) ? null : user.Value;
        }

        public void SaveOrUpdateUser(ApplicationUser user)
        {
            _inMemoryRepository.AddOrUpdate(user.Email, user, (_, _) => user);
        }

        public void DeleteUser(Guid id)
        {
            var user = _inMemoryRepository.FirstOrDefault(u => u.Value.Id == id);
            if (!string.IsNullOrWhiteSpace(user.Key)) _inMemoryRepository.TryRemove(user.Key, out _);
        }
    }
}
