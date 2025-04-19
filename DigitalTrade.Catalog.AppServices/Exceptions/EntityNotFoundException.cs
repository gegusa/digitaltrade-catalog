namespace DigitalTrade.Catalog.AppServices.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string msg) : base(msg)
    {
    }
}