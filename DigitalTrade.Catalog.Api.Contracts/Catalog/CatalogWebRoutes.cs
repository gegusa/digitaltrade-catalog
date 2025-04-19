namespace DigitalTrade.Catalog.Api.Contracts.Catalog;

public static class CatalogWebRoutes
{
    public const string BasePath = "catalog";

    public const string CreateProduct = "create-product";

    public const string UpdateProduct = "update-product";

    public const string DeleteProduct = "delete-product";

    public const string GetProducts = "products";

    public const string GetProductById = $"{GetProducts}/{{id}}";

    public const string GetProductsByCategory = $"{GetProducts}/category/{{category}}";
}