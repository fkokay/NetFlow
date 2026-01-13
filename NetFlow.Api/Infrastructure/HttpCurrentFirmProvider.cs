using NetFlow.Domain.Common;

namespace NetFlow.Api.Infrastructure
{
    public sealed class HttpCurrentFirmProvider : ICurrentFirmProvider
    {
        private readonly IHttpContextAccessor _http;

        public HttpCurrentFirmProvider(IHttpContextAccessor http)
        {
            _http = http;
        }

        public string GetFirmCode()
        {
            return "02";
        }

        public int GetFirmId()
        {
            //var claim = _http.HttpContext?.User?.FindFirst("FirmId")?.Value;

            //if (claim == null)
            //    throw new Exception("FirmId claim bulunamadı.");

            //return int.Parse(claim);

            return 2;
        }
    }
}
