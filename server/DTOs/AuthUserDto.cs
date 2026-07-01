using System.ComponentModel.DataAnnotations;

public class LoginUserDto
{
    [Required]
    [StringLength(6, MinimumLength = 6)]
    public string UserCode {get; set;} = string.Empty;
}