/**
* This file defines the transfer object for creating or editing a station. 
* It contains the properties that are required to create or edit a station.
*/
    
public class CreateStationDto
{
    public int Number { get; set; }
    public int x {get; set;}
    public int y {get; set;}
    public int capacity {get; set;}
}