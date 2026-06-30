using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

public record GenerateTokenRequest
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Email { get; init; }
    public string? Role { get; init; }
}

public interface IJwtService
{
    string generateToken(GenerateTokenRequest request);
}

public class TerminalJwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public TerminalJwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string generateToken(GenerateTokenRequest request)
    {
        List<Claim> claims = JwtClaims.GetDefault(request.Id);
        claims.AddRange(
            new List<Claim> { new Claim("terminal", request.Name) }
        );

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!)
        );

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            signingCredentials: credentials,
            expires: DateTime.UtcNow.AddDays(1),
            claims: claims
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class UserJwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public UserJwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string generateToken(GenerateTokenRequest request)
    {
        return "";
    }
}

public static class JwtClaims
{
    public static List<Claim> GetDefault(Guid Id)
    {
        return new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Exp, 
                DateTimeOffset.UtcNow.AddDays(1).ToUnixTimeSeconds().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, 
                DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString())
        };
    }
}