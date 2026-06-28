using System.ComponentModel.DataAnnotations;

public enum Roles
{
    ADMIN,
    WAITER,
    KITCHEN,
    DESK

}

public class User
{
    public Guid Id {get; set;}

    [Required]
    public Guid RestaurantId {get; set;}

    public Restaurant Restaurant {get; set;} = null!;  

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Name {get; set;} = string.Empty;

    [Required]
    public Roles Rol {get; set;} = Roles.WAITER;

    public DateTime CreateAt {get;} = DateTime.UtcNow;
} 