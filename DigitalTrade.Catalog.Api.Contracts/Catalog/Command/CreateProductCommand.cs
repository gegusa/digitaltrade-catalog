﻿namespace DigitalTrade.Catalog.Api.Contracts.Catalog.Command;

public class CreateProductCommand
{
    public string Name { get; set; }

    public string Category { get; set; }

    public string Description { get; set; }

    public string ImageFile { get; set; }

    public decimal Price { get; set; }
}