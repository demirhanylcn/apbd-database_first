using database_first.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace database_first.Repository;

public interface ITripRepository
{
    public Task<int> DeleteClient(int idClient);
    public Task<bool> CheckTrips(int idClient);
    public  Task<bool> CheckDateIsFuture([FromBody] ClientTripDTO clientTripDto);
    public  Task<bool> CheckTripExists([FromBody] ClientTripDTO clientTripDto);
    public  Task<bool> CheckIfPeselAssigned([FromBody] ClientTripDTO clientTripDto);
    public  Task<bool> CheckClientExists([FromBody] ClientTripDTO clientTripDto);
    public  Task<int> AssignClientTrip([FromBody] ClientTripDTO clientTripDto, int clientId);
    public  Task<int> AddClient([FromBody] ClientTripDTO clientTripDto);
    public Task<PaginatedTrips> GetPaginatedTrips(int pageNum = 1, int pageSize = 10);
}