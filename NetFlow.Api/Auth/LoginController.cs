using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Auth;
using NetFlow.Infrastructure.Identity;
using System.Security.Claims;

namespace NetFlow.Api.Auth
{
    [ApiController]
    [Route("api/auth")]

    public sealed class AuthController : ControllerBase
    {
        private readonly IUserService _users;
        private readonly ITokenService _tokens;

        public AuthController(IUserService users, ITokenService tokens)
        {
            _users = users;
            _tokens = tokens;
        }

        public sealed record LoginRequest(string Email, string Password,string FirmCode);
        public sealed record LoginResponse(string Token);

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest req)
        {
            var user = await _users.Authenticate(req.Email, req.Password,req.FirmCode);
            var token = _tokens.CreateToken(user);
            return Ok(new LoginResponse(token));
        }

        [HttpPost("login-web")]
        public async Task<IActionResult> LoginWeb(LoginRequest req)
        {
            var user = await _users.Authenticate(req.Email, req.Password, req.FirmCode);
            if (user == null)
                return Unauthorized();

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Email),
        new Claim("UserId", user.Id.ToString()),
        new Claim("FirmCode", req.FirmCode)
    };

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("Cookies", principal);

            return Ok();
        }
    }
}
