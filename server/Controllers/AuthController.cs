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

    [HttpPost("login/terminal")]
    public async Task<ActionResult<string>> LoginTerminal([FromBody] LoginTerminalDto dto)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        string token = await _service.LoginTerminalAsync(dto);

        return Ok(token);
    }

    [HttpPost("login/user")]
    public async Task<ActionResult<string>> LoginUser([FromBody] LoginUserDto dto)
    {
        if(!ModelState.IsValid) return ValidationProblem(ModelState);

        string token = await _service.LoginUserAsync(dto);

        return Ok(token);
    }
}