using DigitalTrade.Catalog.Api.Contracts.Catalog.Command;
using DigitalTrade.Catalog.Api.Contracts.Catalog.Request;
using DigitalTrade.Catalog.Api.Contracts.Catalog.Response;

namespace DigitalTrade.Catalog.AppServices.Catalog;

public interface ICatalogHandler
{
    public Task<CreateProductResponse> CreateProduct(CreateProductCommand command, CancellationToken ct);

    public Task<UpdateProductResponse> UpdateProduct(UpdateProductCommand command, CancellationToken ct);

    public Task<DeleteProductResponse> DeleteProduct(DeleteProductCommand command, CancellationToken ct);

    public Task<GetProductByIdResponse> GetProductById(GetProductByIdRequest request, CancellationToken ct);

    public Task<GetProductByCategoryResponse> GetProductByCategory(GetProductByCategoryRequest request, CancellationToken ct);

    public Task<GetProductsResponse> GetProducts(GetProductsRequest request, CancellationToken ct);
}