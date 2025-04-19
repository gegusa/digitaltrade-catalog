namespace DigitalTrade.Catalog.Api.Contracts.Catalog.Request;

public class GetProductsRequest
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}