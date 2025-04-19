using DigitalTrade.Catalog.Api.Contracts.Catalog.Dto;
using DigitalTrade.Catalog.Entities.Entities;

namespace DigitalTrade.Catalog.AppServices.Mappers;

internal static class ProductMapper
{
    public static Product ToProduct(this ProductEntity entity)
    {
        return new Product
        {
            Id = entity.Id,
            Name = entity.Name,
            Category = entity.Category,
            Description = entity.Description,
            ImageFile = entity.ImageFile,
            Price = entity.Price
        };
    }
}