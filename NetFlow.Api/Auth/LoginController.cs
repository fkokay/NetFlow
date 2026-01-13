using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetFlow.Application.Auth;

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
    }
}
