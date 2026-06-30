using System.ComponentModel.DataAnnotations;

public class Station
{
    public Guid Id { get; set; }

    [Required]
    public Guid AccountId {get; set;}

    public Account Account {get; set;} = null!;
    
    [Required]
    [Range(1, int.MaxValue)]
    public int Number { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int X {get; set;}

    [Required]
    [Range(0, int.MaxValue)]
    public int Y {get; set;}

    [Required]
    [Range(2, int.MaxValue)]
    public int Capacity {get; set;}
}