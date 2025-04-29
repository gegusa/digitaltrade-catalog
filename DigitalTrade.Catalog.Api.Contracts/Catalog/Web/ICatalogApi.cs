using DigitalTrade.Catalog.Api.Contracts.Catalog.Command;
using DigitalTrade.Catalog.Api.Contracts.Catalog.Request;
using DigitalTrade.Catalog.Api.Contracts.Catalog.Response;
using Refit;

namespace DigitalTrade.Catalog.Api.Contracts.Catalog.Web;

public interface ICatalogApi
{
    [Post("/catalog/create-product")]
    Task<CreateProductResponse> CreateProduct([Body] CreateProductCommand command, CancellationToken ct);

    [Post("/catalog/update-product")]
    Task<UpdateProductResponse> UpdateProduct([Body] UpdateProductCommand command, CancellationToken ct);

    [Post("/catalog/delete-product")]
    Task<DeleteProductResponse> DeleteProduct([Body] DeleteProductCommand command, CancellationToken ct);

    [Get("/catalog/products")]
    Task<GetProductsResponse> GetProducts([Query] GetProductsRequest request, CancellationToken ct);

    [Get("/catalog/products/{id}")]
    Task<GetProductByIdResponse> GetProductById(long id, CancellationToken ct);

    [Get("/catalog/products/category/{category}")]
    Task<GetProductsByCategoryResponse> GetProductsByCategory(string category, CancellationToken ct);
}