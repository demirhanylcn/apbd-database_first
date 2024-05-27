using database_first.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace database_first.Services;

public interface ITripService
{
    public Task<int> DeleteClient(int idClient);
    public  Task<int> AssignClientTrip([FromBody] ClientTripDTO clientTripDto);
    public  Task ValidateClientTrip(ClientTripDTO clientTripDto);
    public  Task<PaginatedTrips> GetPaginatedTrips(int pageNum = 1, int pageSize = 10);
}