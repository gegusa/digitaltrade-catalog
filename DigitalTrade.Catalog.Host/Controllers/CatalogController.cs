using DigitalTrade.Catalog.Api.Contracts.Catalog;
using DigitalTrade.Catalog.Api.Contracts.Catalog.Command;
using DigitalTrade.Catalog.Api.Contracts.Catalog.Request;
using DigitalTrade.Catalog.Api.Contracts.Catalog.Response;
using DigitalTrade.Catalog.AppServices.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalTrade.Catalog.Host.Controllers;

[ApiController]
[Route(CatalogWebRoutes.BasePath)]
public class CatalogController : ControllerBase
{
    private readonly ICatalogHandler _handler;

    public CatalogController(ICatalogHandler handler)
    {
        _handler = handler;
    }

    [HttpPost(CatalogWebRoutes.CreateProduct)]
    public Task<CreateProductResponse> CreateProduct([FromBody] CreateProductCommand command, CancellationToken ct)
    {
        return _handler.CreateProduct(command, ct);
    }

    [HttpPost(CatalogWebRoutes.UpdateProduct)]
    public Task<UpdateProductResponse> UpdateProduct([FromBody] UpdateProductCommand command, CancellationToken ct)
    {
        return _handler.UpdateProduct(command, ct);
    }

    [HttpPost(CatalogWebRoutes.DeleteProduct)]
    public Task<DeleteProductResponse> DeleteProduct([FromBody] DeleteProductCommand command, CancellationToken ct)
    {
        return _handler.DeleteProduct(command, ct);
    }

    [HttpGet(CatalogWebRoutes.GetProducts)]
    public Task<GetProductsResponse> GetProducts([FromQuery] GetProductsRequest request, CancellationToken ct)
    {
        return _handler.GetProducts(request, ct);
    }

    [HttpGet(CatalogWebRoutes.GetProductById)]
    public Task<GetProductByIdResponse> GetProductById([FromRoute] long id, CancellationToken ct)
    {
        return _handler.GetProductById(new GetProductByIdRequest
        {
            Id = id
        }, ct);
    }

    [AllowAnonymous]
    [HttpGet(CatalogWebRoutes.GetProductsByCategory)]
    public Task<GetProductsByCategoryResponse> GetProductsByCategory([FromRoute] string category, CancellationToken ct)
    {
        return _handler.GetProductsByCategory(new GetProductsByCategoryRequest
        {
            Category = category
        }, ct);
    }
}