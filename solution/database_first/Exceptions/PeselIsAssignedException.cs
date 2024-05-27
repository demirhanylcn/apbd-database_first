using database_first.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace database_first.Exceptions;

public class PeselIsAssignedException : Exception
{
    public PeselIsAssignedException([FromBody] ClientTripDTO clientTripDto) : base(
        $"this pesel {clientTripDto.Pesel} is already assigned to given trip {clientTripDto.IdTrip}.")
    {
        
    }
}