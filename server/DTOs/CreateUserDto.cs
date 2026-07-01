using System.ComponentModel.DataAnnotations;

public class CreateUserDto
{
    [Required]
    public string Name{ get; set;} = null!;

    [Required]
    [StringLength(6, MinimumLength = 6)]
    public string Code {get; set;} = null!;

    [Required]
    public Roles Rol = Roles.WAITER; 
}