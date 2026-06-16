using System.ComponentModel.DataAnnotations;

public class Station
{
    public int Id { get; set; }
    public int Number { get; set; }
    [Required]
    public int x {get; set;}
    [Required]
    public int y {get; set;}
    [Required]
    public int capacity {get; set;}
}