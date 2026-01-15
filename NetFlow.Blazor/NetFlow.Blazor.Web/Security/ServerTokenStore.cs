namespace NetFlow.Blazor.Web.Security
{
    public class ServerTokenStore : ITokenStore
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServerTokenStore(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetToken()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst("access_token")?.Value;
        }
    }
}
