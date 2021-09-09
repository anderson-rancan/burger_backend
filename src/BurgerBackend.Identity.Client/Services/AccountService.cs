using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BurgerBackend.Identity.Interface;
using BurgerBackend.Identity.Interface.Services.Models;

namespace BurgerBackend.Identity.Client.Services
{
    public sealed class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Guid?> ValidateToken(string token, CancellationToken cancellationToken = default)
        {
            var content = new StringContent(token, Encoding.UTF8, "application/json");

            var result = await _httpClient.PostAsync(Endpoints.Account.ValidateToken, content, cancellationToken);

            return result.StatusCode == System.Net.HttpStatusCode.OK
                ? Guid.Parse(JsonSerializer.Deserialize<string>(await result.Content.ReadAsStringAsync(cancellationToken), new JsonSerializerOptions(JsonSerializerDefaults.Web)))
                : null;
        }

        public async Task<GetUserResponse> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var query = Endpoints.Account.GetById.Replace("{id}", id.ToString());

            var result = await _httpClient.GetStringAsync(query, cancellationToken);

            return JsonSerializer.Deserialize<GetUserResponse>(result, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }
    }
}
