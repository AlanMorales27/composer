using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserService _service;
    public UserController(UserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        List<User> users = await _service.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(Guid id)
    {
        User? user = await _service.GetUserAsync(id);

        if(user is null) return NotFound();

        return Ok(user);
    }

    [HttpPost]
    [Authorize(Policy = "TerminalToken")]
    public async Task<ActionResult<User>> CreateUser([FromBody] CreateUserDto dto)
    {
        Guid accountId = Guid.Parse(User.FindFirst(JwtRegisteredClaimNames.Sub)!.Value); 
        
        User createdUser = await _service.CreateUserAsync(dto, accountId); 

        return CreatedAtAction(
            nameof(GetUser),
            new { id = createdUser.Id },
            createdUser
        );
    }

}