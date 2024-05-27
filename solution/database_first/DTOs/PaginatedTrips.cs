using database_first.Models;

namespace database_first.DTOs;

public class PaginatedTrips
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public int AllPages { get; set; }
    public List<Trip> Trips { get; set; } = [];
}