using DigitalTrade.Catalog.Api.Contracts.Catalog.Dto;
using DigitalTrade.Catalog.Api.Contracts.Catalog.Kafka.Events;
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

    public static ProductUpdatedEvent ToProductUpdatedEvent(this ProductEntity entity)
    {
        return new ProductUpdatedEvent
        {
            ProductId  = entity.Id,
            Name = entity.Name,
            Category = entity.Category,
            Description = entity.Description,
            ImageFile = entity.ImageFile,
            Price = entity.Price
        };
    }
}