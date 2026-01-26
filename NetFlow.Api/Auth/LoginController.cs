using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Auth;
using NetFlow.Domain.Identity.Exceptions;
using NetFlow.Infrastructure.Identity;
using System.Security.Claims;

namespace NetFlow.Api.Auth
{
    [ApiController]
    [Route("api/auth")]

    public sealed class AuthController(IUserService users, ITokenService tokens) : ControllerBase
    {
        public sealed record LoginRequest(string Email, string Password, string FirmCode);
        public sealed record LoginResponse(string Token, string? ErrorMessage = null, string? ExceptionMessage = null);

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest req)
        {
            try
            {
                var user = await users.Authenticate(req.Email, req.Password, req.FirmCode);
                var token = tokens.CreateToken(user);

                return Ok(new LoginResponse(token));
            }
            catch (InvalidLoginException ex)
            {
                return Unauthorized(new LoginResponse(
                    Token: "",
                    ErrorMessage: ex.Message
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new LoginResponse(
                    Token: "",
                    ErrorMessage: "Beklenmeyen bir hata oluştu",
                    ExceptionMessage: ex.Message
                ));
            }
        }
    }
}
