using database_first.DTOs;
using database_first.Exceptions;
using database_first.Repository;
using Microsoft.AspNetCore.Mvc;

namespace database_first.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;

    public TripService(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async Task<int> DeleteClient(int idClient)
    {
        var result = await _tripRepository.CheckTrips(idClient);
        if (result) throw new ClientHasTripException(idClient);


        return await _tripRepository.DeleteClient(idClient);
    }

    public async Task<int> AssignClientTrip([FromBody] ClientTripDTO clientTripDto)
    {
        await ValidateClientTrip(clientTripDto);
        var clientId = await _tripRepository.AddClient(clientTripDto);
        return await _tripRepository.AssignClientTrip(clientTripDto, clientId);
    }

    public async Task ValidateClientTrip(ClientTripDTO clientTripDto)
    {
         await _tripRepository.CheckClientExists(clientTripDto);


         await _tripRepository.CheckIfPeselAssigned(clientTripDto);


        await _tripRepository.CheckTripExists(clientTripDto);


         await _tripRepository.CheckDateIsFuture(clientTripDto);
    }

    public async Task<PaginatedTrips> GetPaginatedTrips(int pageNum = 1, int pageSize = 10)
    {
        var result = await _tripRepository.GetPaginatedTrips(pageNum, pageSize);
        return result;
    }
}