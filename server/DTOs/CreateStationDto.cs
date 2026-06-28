/**
* This file defines the transfer object for creating or editing a station. 
* It contains the properties that are required to create or edit a station.
*/
namespace Server.DTOs;
public class CreateStationDto
{
    public int Number { get; set; }
    public int X {get; set;}
    public int Y {get; set;}
    public int Capacity {get; set;}
}

