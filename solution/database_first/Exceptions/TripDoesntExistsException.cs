namespace database_first.Exceptions;

public class TripDoesntExistsException : Exception
{
    public TripDoesntExistsException(int tripId) : base($"trip with given id {tripId} doesnt exists.")
    {
        
    }
}