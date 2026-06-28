using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;
using Server.DTOs;

public class AuthService
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public async Task RegisterAsync(RegisterDto data)
    {
        if (data is null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        if (string.IsNullOrWhiteSpace(data.Name))
        {
            throw new ArgumentException(
                "Name is required.", 
                nameof(data.Name)
            );
        }

        if (string.IsNullOrWhiteSpace(data.Password))
        {
            throw new ArgumentException(
                "Password is required.", 
                nameof(data.Password)
            );
        }
        
        bool emailExist = await _context.Restaurants.AnyAsync( 
            r => r.Email == data.Email
        );
      
        if(emailExist) throw new InvalidOperationException( "Email already exist. ");
        
        Restaurant newRegister = new Restaurant
        {
            Name = data.Name.Trim(),
            Email = data.Email.Trim(),
            Password = data.Password,
            Phone = data.Phone?.Trim(),
        };

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

    public async Task LoginAsync( LoginDto data )
    {
        if(data is null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        if (string.IsNullOrWhiteSpace(data.Email))
        {
            throw new ArgumentException(
                "Email is required.", 
                nameof(data.Email)
            );
        }

        if (string.IsNullOrWhiteSpace(data.Password))
        {
            throw new ArgumentException(
                "Password is required.", 
                nameof(data.Password)
            );
        }

        Restaurant? register = await _context.Restaurants.FirstOrDefaultAsync(
            reg => reg.Email == data.Email
        );

        if(register is null || register.Password != data.Password)
        {
            throw new AuthenticationException("Invalid credentials");
        }

    }
}