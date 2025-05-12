using DigitalTrade.Catalog.Api.Contracts.Catalog.Command;
using DigitalTrade.Catalog.Api.Contracts.Catalog.Kafka;
using DigitalTrade.Catalog.Api.Contracts.Catalog.Kafka.Events;
using DigitalTrade.Catalog.Api.Contracts.Catalog.Request;
using DigitalTrade.Catalog.Api.Contracts.Catalog.Response;
using DigitalTrade.Catalog.AppServices.Exceptions;
using DigitalTrade.Catalog.AppServices.Mappers;
using DigitalTrade.Catalog.Entities;
using DigitalTrade.Catalog.Entities.Entities;
using KafkaFlow.Producers;
using LinqToDB;

namespace DigitalTrade.Catalog.AppServices.Catalog;

internal class CatalogHandler : ICatalogHandler
{
    private readonly CatalogDataConnection _db;
    private readonly IProducerAccessor _producers;

    public CatalogHandler(CatalogDataConnection db, IProducerAccessor producerAccessor)
    {
        _db = db;
        _producers = producerAccessor;
    }

    public async Task<CreateProductResponse> CreateProduct(CreateProductCommand command, CancellationToken ct)
    {
        var entity = new ProductEntity
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        return new CreateProductResponse
        {
            ProductId = await _db.InsertWithInt64IdentityAsync(entity, token: ct)
        };
    }

    public async Task<UpdateProductResponse> UpdateProduct(UpdateProductCommand command, CancellationToken ct)
    {
        var entity = await _db.Products.FirstOrDefaultAsync(e => e.Id == command.Id, ct);

        if (entity is null)
            throw new EntityNotFoundException($"Product with id={command.Id} not found");

        if (command.Name is not null)
            entity.Name = command.Name;
        if (command.Category is not null)
            entity.Category = command.Category;
        if (command.Description is not null)
            entity.Description = command.Description;
        if (command.Price is not null)
            entity.Price = command.Price.Value;
        if (command.ImageFile is not null)
            entity.ImageFile = command.ImageFile;

        await _db.UpdateAsync(entity, token: ct);

        var productUpdatedEvent = entity.ToProductUpdatedEvent();
        await _producers[Topics.CatalogChangedProducerName].ProduceAsync(
            Topics.CatalogChangedName,
            productUpdatedEvent.ProductId.ToString(),
            productUpdatedEvent);

        return new UpdateProductResponse
        {
            IsSuccess = true
        };
    }

    public async Task<DeleteProductResponse> DeleteProduct(DeleteProductCommand command, CancellationToken ct)
    {
        var entity = await _db.Products.FirstOrDefaultAsync(e => e.Id == command.ProductId, ct);

        if (entity is null)
            throw new EntityNotFoundException($"Product with id={command.ProductId} not found");

        await _db.DeleteAsync(entity, token: ct);

        await _producers[Topics.CatalogChangedProducerName].ProduceAsync(
            Topics.CatalogChangedName,
            command.ProductId.ToString(),
            new ProductDeletedEvent
            {
                ProductId = command.ProductId
            });

        return new DeleteProductResponse
        {
            IsSuccess = true
        };
    }

    public async Task<GetProductByIdResponse> GetProductById(GetProductByIdRequest request, CancellationToken ct)
    {
        var entity = await _db.Products.FirstOrDefaultAsync(e => e.Id == request.Id, ct);

        if (entity is null)
            throw new EntityNotFoundException($"Product with id={request.Id} not found");

        return new GetProductByIdResponse
        {
            Product = entity.ToProduct()
        };
    }

    public async Task<GetProductsByCategoryResponse> GetProductsByCategory(GetProductsByCategoryRequest request,
        CancellationToken ct)
    {
        var entities = await _db.Products
            .Where(e =>
                e.Category.Contains(request.Category))
            .ToArrayAsync(ct);

        return new GetProductsByCategoryResponse
        {
            Products = entities.Select(e => e.ToProduct()).ToArray()
        };
    }

    public async Task<GetProductsResponse> GetProducts(GetProductsRequest request, CancellationToken ct)
    {
        var productEntities = await _db.Products
            .OrderBy(p => p.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(ct);

        return new GetProductsResponse
        {
            Products = productEntities.Select(e => e.ToProduct()).ToArray()
        };
    }
}