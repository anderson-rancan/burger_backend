using System.Linq;
using System.Threading.Tasks;
using BurgerBackend.Identity.Client.Services;
using Microsoft.AspNetCore.Http;

namespace BurgerBackend.Identity.Client.Middlewares
{
    public sealed class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        public readonly IAccountService _accountService;

        public JwtMiddleware(RequestDelegate next, IAccountService accountService)
        {
            _next = next;
            _accountService = accountService;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrWhiteSpace(token))
            {
                var userId = await _accountService.ValidateToken(token);

                if (userId != null)
                {
                    context.Items["User"] = await _accountService.GetById(userId.Value);
                }
            }

            await _next(context);
        }
    }
}
