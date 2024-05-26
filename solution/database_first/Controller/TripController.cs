using database_first.Services;
using Microsoft.AspNetCore.Mvc;

namespace database_first.Controller;


[ApiController]
[Route("api/[controller]")]
public class TripController : ControllerBase
{
    readonly  ITripService _TripService;

    public TripController(ITripService tripService)
    {
        _TripService = tripService;
    }

    // [HttpDelete]
    // public async Task<IActionResult> DeleteClient(int? idClient)
    // {
    //    
    // }
}