namespace database_first.Exceptions;

public class ClientNullException : Exception
{
    public ClientNullException(int clientId) : base($"there is no client with given id {clientId}")
    {
        
    }
}