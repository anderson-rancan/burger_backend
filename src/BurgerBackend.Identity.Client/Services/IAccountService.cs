using System;
using System.Threading;
using System.Threading.Tasks;
using BurgerBackend.Identity.Interface.Services.Models;

namespace BurgerBackend.Identity.Client.Services
{
    public interface IAccountService
    {
        Task<Guid?> ValidateToken(string token, CancellationToken cancellationToken = default);
        Task<GetUserResponse> GetById(Guid id, CancellationToken cancellationToken = default);
    }
}
