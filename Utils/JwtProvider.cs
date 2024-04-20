using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtExample.Abstractions;
using JwtExample.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JwtExample.Utils;

public class JwtProvider : IJwtProvider
{
    private readonly JwtSettings _jwtSettings;

    public JwtProvider(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string Generate(string email)
    {
        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Email, email),
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddSeconds(20),
            signingCredentials: signingCredentials
        );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}
