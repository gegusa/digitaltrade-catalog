using DigitalTrade.Catalog.Entities.Entities;
using LinqToDB;
using LinqToDB.Data;

namespace DigitalTrade.Catalog.Entities;

/// <summary>
/// Абстракция подключения к базе данных.
/// </summary>
public class CatalogDataConnection : DataConnection
{
    public CatalogDataConnection(DataOptions<CatalogDataConnection> options)
        : base(options.Options)
    {
    }

    /// <summary>
    /// Таблица Продукты.
    /// </summary>
    public ITable<ProductEntity> Products => this.GetTable<ProductEntity>();
}