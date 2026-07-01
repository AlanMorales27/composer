using Microsoft.EntityFrameworkCore;
using Server.DTOs;

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

    public async Task<Station?> GetStationAsync(Guid id)
    {
        return await _context.Stations.FindAsync(id);
    }

    public async Task<Station> CreateStationAsync(CreateStationDto station)
    {
        Station newStation = new Station {
            Number = station.Number,
            X = station.X,
            Y = station.Y,
            Capacity = station.Capacity
        };

        _context.Stations.Add(newStation);

        await _context.SaveChangesAsync();

        return newStation;
    }

    public async Task<Station?> UpdateStationAsync(Guid id, CreateStationDto station)
    {
        Station? existingStation = await GetStationAsync(id);

        if(existingStation is null) return null;
        
        existingStation.Number = station.Number;
        existingStation.X = station.X;
        existingStation.Y = station.Y;
        existingStation.Capacity = station.Capacity;

        await _context.SaveChangesAsync();

        return existingStation;
    }

    public async Task<bool> DeleteStationAsync(Guid id)
    {
        Station? station = await GetStationAsync(id);

        if(station is null) return false;

        _context.Stations.Remove(station);
        await _context.SaveChangesAsync();

        return true;
    }

}