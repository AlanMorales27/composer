using Microsoft.AspNetCore.Mvc;
using Server.DTOs;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _service;

    public AuthController(AuthService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto data)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        await _service.RegisterAsync(data);

        return Ok(new { message = "Registration completed successfully." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto data)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        await _service.LoginAsync(data);

        return Ok(new { message = "Login completed successfully." });
    }
}