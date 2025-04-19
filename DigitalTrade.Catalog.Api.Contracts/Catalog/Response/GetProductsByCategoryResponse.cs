using DigitalTrade.Catalog.Api.Contracts.Catalog.Dto;

namespace DigitalTrade.Catalog.Api.Contracts.Catalog.Response;

public class GetProductsByCategoryResponse
{
    public Product[] Products { get; set; }
}