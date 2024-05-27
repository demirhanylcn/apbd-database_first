using database_first.DTOs;
using database_first.Exceptions;
using database_first.Models;
using database_first.Services;
using Microsoft.AspNetCore.Mvc;

namespace database_first.Controller;

[ApiController]
[Route("api/")]
public class TripController : ControllerBase
{
    readonly ITripService _tripService;

    public TripController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [HttpDelete("clients/{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        try
        {
            var result = await _tripService.DeleteClient(idClient);
            return Ok(result);
        }
        catch (ClientHasTripException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ClientNullException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("trips/{idTrip}/clients")]
    public async Task<IActionResult> AssignClientTrip([FromBody] ClientTripDTO clientTripDto)

    {
        try
        {
            var result = await _tripService.AssignClientTrip(clientTripDto);
            return Ok(result);
        }
        catch (TripIsOccured ex)
        {
            return BadRequest(ex.Message);
        }
        catch (TripDoesntExistsException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (PeselIsAssignedException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ClientWithPeselExistsException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("trips")]
    public async Task<IActionResult> GetPaginatedTrips([FromQuery] int? pageNum, int? pageSize)
    {
        if (pageNum is null or < 1) pageNum = 1;
        if (pageSize is null or < 10) pageSize = 10;
        var result = await _tripService.GetPaginatedTrips(pageNum.Value, pageSize.Value);
        return Ok(result);
    }
}