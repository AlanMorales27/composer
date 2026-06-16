using Microsoft.EntityFrameworkCore;

public class StationService
{
    private readonly AppDbContext _context;

    public StationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Station>> GetStationsAsync()
    {

        return await _context.Stations.ToListAsync();
    }

    public async Task<Station> GetStationAsync(int id)
    {
        Station? station = await _context.Stations.FindAsync(id);

        if(station is null) throw new Exception("Station not found");

        return station;
    }

    public async Task<Station> CreateStationAsync(CreateStationDto station)
    {
        Station newStation = new Station {
            Number = station.Number,
            x = station.x,
            y = station.y,
            capacity = station.capacity
        };

        _context.Stations.Add(newStation);

        await _context.SaveChangesAsync();

        return newStation;
    }

    public async Task<Station?> UpdateStationAsync(int id, CreateStationDto station)
    {
        Station? existingStation = await this.GetStationAsync(id);
        
        existingStation.Number = station.Number;
        existingStation.x = station.x;
        existingStation.y = station.y;
        existingStation.capacity = station.capacity;

        await _context.SaveChangesAsync();

        return existingStation;
    }

    public async Task<bool> DeleteStationAsync(int id)
    {
        Station? station = await this.GetStationAsync(id);

        _context.Stations.Remove(station);
        await _context.SaveChangesAsync();

        return true;
    }

}