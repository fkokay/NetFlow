
using NetFlow.Domain.Common;
using System.Net;

namespace NetFlow.Api.Middlewares
{
    public class DomainExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (DomainException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/problem+json";

                await context.Response.WriteAsJsonAsync(new
                {
                    type = "domain-error",
                    title = ex.Code,
                    status = 400,
                    detail = ex.Message
                });
            }
        }
    }
}
