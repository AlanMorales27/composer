public record GenerateTokenRequest
{
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
        return "";
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
