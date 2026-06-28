using System.ComponentModel.DataAnnotations;

public class Restaurant
{
    public int Id {get; set;}

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name {get; set;} = string.Empty;

    [EmailAddress]
    public string? Email {get; set;} = string.Empty;

    [Required]
    public string Password {get;set;} = string.Empty;

    public string? Phone {get;set;} = string.Empty;

    public DateTime CreateAt {get; set;} = DateTime.UtcNow;
}   