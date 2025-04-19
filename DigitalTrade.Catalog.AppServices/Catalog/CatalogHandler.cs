using DigitalTrade.Catalog.Api.Contracts.Catalog.Command;
using DigitalTrade.Catalog.Api.Contracts.Catalog.Request;
using DigitalTrade.Catalog.Api.Contracts.Catalog.Response;
using DigitalTrade.Catalog.AppServices.Exceptions;
using DigitalTrade.Catalog.AppServices.Mappers;
using DigitalTrade.Catalog.Entities;
using DigitalTrade.Catalog.Entities.Entities;
using LinqToDB;

namespace DigitalTrade.Catalog.AppServices.Catalog;

internal class CatalogHandler : ICatalogHandler
{
    private readonly CatalogDataConnection _db;

    public CatalogHandler(CatalogDataConnection db)
    {
        _db = db;
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

        entity.Name = command.Name;
        entity.Category = command.Category;
        entity.Description = command.Description;
        entity.Price = command.Price;
        entity.ImageFile = command.ImageFile;

        await _db.UpdateAsync(entity, token: ct);

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

    public async Task<GetProductsByCategoryResponse> GetProductsByCategory(GetProductsByCategoryRequest request, CancellationToken ct)
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