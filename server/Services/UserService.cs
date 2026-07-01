using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserService
{
    private AppDbContext _context;
    private IPasswordHasher<User> _hasher;

    public UserService(AppDbContext context, IPasswordHasher<User> hasher)
    {
        _context = context;
        _hasher = hasher;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUserAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    /*
    * Account id is into the JWT of the Terminal. It could be get direct 
    * in the controller
    */ 
    public async Task<User> CreateUserAsync(CreateUserDto dto, Guid accountId)
    {
        ArgumentNullException.ThrowIfNull(dto);
        ArgumentException.ThrowIfNullOrWhiteSpace(dto.Name, nameof(dto.Name));
        ArgumentException.ThrowIfNullOrWhiteSpace(dto.Code, nameof(dto.Code));
        ArgumentException.ThrowIfNullOrWhiteSpace(dto.Rol.ToString(), nameof(dto.Rol));

        User newUser = new User
        {
            AccountId = accountId,
            Code = "",
            Name = dto.Name,
            Rol = dto.Rol,
        };

        newUser.Code = _hasher.HashPassword(newUser, dto.Code);

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return newUser;
    }
}