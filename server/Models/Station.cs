using System.ComponentModel.DataAnnotations;

public class Station
{
    public int Id { get; set; }
    public int Number { get; set; }
    [Required]
    public int X {get; set;}
    [Required]
    public int Y {get; set;}
    [Required]
    public int Capacity {get; set;}
}