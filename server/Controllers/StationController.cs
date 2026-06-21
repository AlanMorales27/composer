
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/stations")]
public class StationController: ControllerBase
{
    public readonly StationService _service;

    public StationController(StationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Station>>> GetStations()
    {
        List<Station> station_list = await _service.GetStationsAsync();

        return Ok(station_list);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Station>> GetStation(int id)
    {
        Station station = await _service.GetStationAsync(id);

        return Ok(station);
    }

    [HttpPost]
    public async Task<ActionResult<Station>> CreateStation([FromBody] CreateStationDto station)
    {
        Station created = await _service.CreateStationAsync(station);

        return CreatedAtAction(
            nameof(GetStation),
            new { id = created.Id },
            created
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Station>> UpdateStation(int id, [FromBody] CreateStationDto station)
    {
        Station? updated = await _service.UpdateStationAsync(id, station);

        if(updated is null) return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteStation(int id)
    {
        await _service.DeleteStationAsync(id);
        
        return Ok();
    }
}