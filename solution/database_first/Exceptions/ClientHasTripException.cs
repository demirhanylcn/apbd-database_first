namespace database_first.Exceptions;

public class ClientHasTripException : Exception
{
    public ClientHasTripException(int clientId) : base($"client with given id {clientId} has trips")
    {
        
    }    
}