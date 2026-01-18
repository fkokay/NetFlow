using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetFlow.Application.Auth;
using NetFlow.Domain.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NetFlow.Infrastructure.Auth
{
    public sealed class JwtTokenService : ITokenService
    {
        private readonly JwtOptions _opt;

        public JwtTokenService(IOptions<JwtOptions> opt)
        {
            _opt = opt.Value;
        }

        public string CreateToken(User user)
        {
            // Snapshot
            var snapshot = new UserSnapshot
            {
                Id = user.Id.Value,
                FullName = user.FullName,
                Email = user.Email,
                FirmId = user.Firm.Id,
                FirmCode = user.Firm.Code,
                FirmName = user.Firm.Name,
                RoleCode = user.Role.Code,
                RoleName = user.Role.Name,
                Permissions = user.Permissions.ToList()
            };

            // Permissions -> tek claim string yapalım (çok claim şişirmesin)
            var perms = string.Join(",", snapshot.Permissions);

            var claims = new List<Claim>
        {
            new(ClaimTypes.Name,snapshot.FullName),
            new(ClaimTypes.NameIdentifier, snapshot.Id.ToString()),
            new(ClaimTypes.Email, snapshot.Email),
            new("FirmId", snapshot.FirmId.ToString()),
            new("FirmCode", snapshot.FirmCode),
            new("FirmName",snapshot.FirmName),
            new(ClaimTypes.Role, snapshot.RoleCode),
            new("RoleName", snapshot.RoleName),
            new("Permissions", perms)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.SigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _opt.Issuer,
                audience: _opt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_opt.ExpMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserSnapshot? ReadSnapshot(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return null;

            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt;

            try
            {
                jwt = handler.ReadJwtToken(token);
            }
            catch
            {
                return null;
            }

            string? name = jwt.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            string? uid = jwt.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            string? email = jwt.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            string? firmId = jwt.Claims.FirstOrDefault(x => x.Type == "FirmId")?.Value;
            string? firmCode = jwt.Claims.FirstOrDefault(x => x.Type == "FirmCode")?.Value;
            string? firmName = jwt.Claims.FirstOrDefault(x => x.Type == "FirmName")?.Value;
            string? role = jwt.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            string? roleName = jwt.Claims.FirstOrDefault(x => x.Type == "RoleName")?.Value;
            string? perms = jwt.Claims.FirstOrDefault(x => x.Type == "Permissions")?.Value;

            if (!int.TryParse(uid, out var id)) return null;
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(firmCode) || string.IsNullOrWhiteSpace(role))
                return null;

            return new UserSnapshot
            {
                Id = id,
                Name = name ?? string.Empty,
                Email = email,
                FirmId = Convert.ToInt32(firmId),
                FirmCode = firmCode ?? string.Empty,
                FirmName = firmName ?? string.Empty,
                RoleCode = role,
                RoleName = roleName ?? role,
                Permissions = string.IsNullOrWhiteSpace(perms)
                    ? new List<string>()
                    : perms.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList()
            };
        }
    }
}
