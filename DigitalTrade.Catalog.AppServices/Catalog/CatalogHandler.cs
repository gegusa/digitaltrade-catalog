using DigitalTrade.Catalog.Api.Contracts.Catalog.Command;
using DigitalTrade.Catalog.Api.Contracts.Catalog.Request;
using DigitalTrade.Catalog.Api.Contracts.Catalog.Response;

namespace DigitalTrade.Catalog.AppServices.Catalog;

public class CatalogHandler : ICatalogHandler
{
    public Task<CreateProductResponse> CreateProduct(CreateProductCommand command, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<UpdateProductResponse> UpdateProduct(UpdateProductCommand command, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<DeleteProductResponse> DeleteProduct(DeleteProductCommand command, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<GetProductByIdResponse> GetProductById(GetProductByIdRequest request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<GetProductByCategoryResponse> GetProductByCategory(GetProductByCategoryRequest request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<GetProductsResponse> GetProducts(GetProductsRequest request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}