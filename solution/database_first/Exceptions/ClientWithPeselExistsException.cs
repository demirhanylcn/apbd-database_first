namespace database_first.Exceptions;

public class ClientWithPeselExistsException : Exception
{
    public ClientWithPeselExistsException(string pesel) : base($"client with pesel number{pesel} exists.")
    {
        
    }
}