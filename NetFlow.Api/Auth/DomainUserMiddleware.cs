using NetFlow.Application.Auth;
using NetFlow.Domain.Identity;
using NetFlow.Infrastructure.Auth;

namespace NetFlow.Api.Auth
{
    public sealed class CurrentUserMiddleware
    {
        private readonly RequestDelegate _next;

        public CurrentUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext http, ITokenService tokenService, CurrentUser current)
        {
            var auth = http.Request.Headers.Authorization.ToString();

            if (!string.IsNullOrWhiteSpace(auth) && auth.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                var token = auth["Bearer ".Length..].Trim();

                var snap = tokenService.ReadSnapshot(token);
                if (snap != null)
                {
                    current.User = UserSnapshotMapper.ToDomain(snap);
                }
            }

            await _next(http);
        }
    }
}
