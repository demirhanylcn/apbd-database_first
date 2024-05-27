using database_first.Context;
using database_first.DTOs;
using database_first.Exceptions;
using database_first.Models;
using Microsoft.AspNetCore.Mvc;
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
        var client = await _tripDbContext.Clients.FirstAsync(e => e.IdClient == idClient);
        _tripDbContext.Remove(client);
        return await _tripDbContext.SaveChangesAsync();
    }

    public async Task<int> AssignClientTrip([FromBody] ClientTripDTO clientTripDto, int clientId)
    {
        

        await _tripDbContext.ClientTrips.AddAsync(new ClientTrip
        {
            IdClient = clientId,
            IdTrip = clientTripDto.IdTrip,
            PaymentDate = clientTripDto.PaymentDate,
            RegisteredAt = DateTime.Now
        });

        return await _tripDbContext.SaveChangesAsync();
    }

    public async Task<int> AddClient([FromBody] ClientTripDTO clientTripDto)
    {
        var client = new Client
        {
            FirstName = clientTripDto.FirstName,
            Email = clientTripDto.Email,
            LastName = clientTripDto.LastName,
            Pesel = clientTripDto.Pesel,
            Telephone = clientTripDto.Telephone
        };
        
        await _tripDbContext.Clients.AddAsync(client);
        await _tripDbContext.SaveChangesAsync();
        return client.IdClient;
    }

    public async Task<bool> CheckClientExists([FromBody] ClientTripDTO clientTripDto)
    {
        var checkClient = await _tripDbContext.Clients.Where(c => c.Pesel == clientTripDto.Pesel).FirstOrDefaultAsync();
        if (checkClient != null) throw new ClientWithPeselExistsException(clientTripDto.Pesel);
        return false;
    }

    public async Task<bool> CheckIfPeselAssigned([FromBody] ClientTripDTO clientTripDto)
    {
        var query = _tripDbContext.Clients.Include(c => c.ClientTrips);
        var result = await query.Where(c => c.Pesel == clientTripDto.Pesel).FirstOrDefaultAsync();
        if (result != null) throw new PeselIsAssignedException(clientTripDto);
        return false;
    }

    public async Task<bool> CheckTripExists([FromBody] ClientTripDTO clientTripDto)
    {
        var result = await _tripDbContext.Trips.Where(t => t.IdTrip == clientTripDto.IdTrip && t.Name == clientTripDto.TripName).FirstOrDefaultAsync();
        if (result == null) throw new TripDoesntExistsException(clientTripDto.IdTrip);
        return false;
    }

    public async Task<bool> CheckDateIsFuture([FromBody] ClientTripDTO clientTripDto)
    {
        var trip = await _tripDbContext.Trips.Where(t => t.IdTrip == clientTripDto.IdTrip)
            .Where(t => t.DateFrom > DateTime.Now).FirstOrDefaultAsync();

        if (trip == null) throw new TripIsOccured(clientTripDto.IdTrip);
        return false;
    }

    public async Task<PaginatedTrips> GetPaginatedTrips(int pageNum = 1, int pageSize = 10)
    {
        var query =  _tripDbContext.Trips.Include(t => t.ClientTrips).ThenInclude(t => t.IdClientNavigation)
            .Include(t => t.IdCountries).OrderBy(t => t.DateFrom);
        var totalPages = await query.CountAsync() / pageSize;
        var trips = await _tripDbContext.Trips
            .Include(e => e.ClientTrips).ThenInclude(e => e.IdClientNavigation)
            .Include(e => e.IdCountries)
            .OrderBy(e => e.DateFrom)
            .Skip(pageNum * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return new PaginatedTrips
        {
            PageSize = pageSize,
            PageNum = pageNum,
            AllPages = totalPages,
            Trips = trips
        };
    } 
}