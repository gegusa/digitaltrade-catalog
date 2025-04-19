using LinqToDB.Mapping;

namespace DigitalTrade.Catalog.Entities.Entities;

[Table(Name = "products", Schema = "catalog")]
public class ProductEntity
{
    [PrimaryKey, Identity, Column("id")] public long Id { get; set; }
    
    [Column("name"), NotNull] public string Name { get; set; } = null!;

    [Column("category"), NotNull] public List<string> Category { get; set; } = [];

    [Column("description"), NotNull] public string Description { get; set; } = null!;

    [Column("image_file"), NotNull] public string ImageFile { get; set; } = null!;

    [Column("price"), NotNull] public decimal Price { get; set; }
}