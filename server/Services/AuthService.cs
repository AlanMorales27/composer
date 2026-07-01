using System.Security.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.DTOs;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<Account> _hasher;
    private readonly TerminalJwtService _tokenService;

    public AuthService(
        AppDbContext context, 
        IPasswordHasher<Account> hasher, 
        TerminalJwtService tokenService
    )
    {
        _context = context;
        _hasher = hasher;
        _tokenService = tokenService;
    }

    public async Task RegisterAsync(RegisterDto data)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentException.ThrowIfNullOrWhiteSpace(data.Name, nameof(data.Name));
        ArgumentException.ThrowIfNullOrWhiteSpace(data.Password, nameof(data.Password));
        
        bool emailExist = await _context.Restaurants.AnyAsync( 
            r => r.Email == data.Email
        );
      
        if(emailExist) throw new InvalidOperationException( "Email already exist. ");
        
        Account newRegister = new Account
        {
            Name = data.Name.Trim(),
            Email = data.Email.Trim(),
            Password = "",
            Phone = data.Phone?.Trim(),
        };
        
        // Hashing password
        newRegister.Password = _hasher.HashPassword(newRegister, data.Password);

        try
        {
            _context.Restaurants.Add(newRegister);
            await _context.SaveChangesAsync();
        }

        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Could not save the registration.", ex);
        }
    }

    public async Task<string> LoginTerminalAsync( LoginTerminalDto data )
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentException.ThrowIfNullOrWhiteSpace(data.Email, nameof(data.Email));
        ArgumentException.ThrowIfNullOrWhiteSpace(data.Password, nameof(data.Password));

        Account? register = await _context.Restaurants.FirstOrDefaultAsync(
            reg => reg.Email == data.Email
        );

        if(register is null)
        {
            throw new AuthenticationException("Invalid credentials");
        }

        var result = _hasher.VerifyHashedPassword(register, register.Password, data.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            throw new AuthenticationException("Invalid credentials");
        }

        return _tokenService.generateToken(
            new GenerateTokenRequest{ 
                Id = register.Id,
                Name = register.Name
            }
        );
    }

    public async Task<string> LoginUserAsync(LoginUserDto dto)
    {
        return "";
    }
}