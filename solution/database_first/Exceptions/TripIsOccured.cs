namespace database_first.Exceptions;

public class TripIsOccured : Exception
{
    public TripIsOccured(int tripId) : base($"trip with given id {tripId} already occured.")
    {
        
    }
}