using database_first.Context;
using database_first.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace database_first.Repository;

public class TripRepository : ITripRepository
{
    private readonly MasterContext _tripDbContext;

    public TripRepository(MasterContext tripDbContext)
    {
        _tripDbContext = tripDbContext;
    }

    public async Task<bool> CheckTrips(int idClient)
    {
        var client = await _tripDbContext.Clients
            .Include(c => c.ClientTrips)
            .FirstOrDefaultAsync(c => c.IdClient == idClient);

        if (client == null)
        {
            throw new ClientNullException(idClient);
        }

        return client.ClientTrips.Any();
    }

    public async Task<int> DeleteClient(int idClient)
    {
        var client = _tripDbContext.Clients.FirstAsync(e => e.IdClient == idClient);
        _tripDbContext.Remove(client);
        return await _tripDbContext.SaveChangesAsync();
    }
}