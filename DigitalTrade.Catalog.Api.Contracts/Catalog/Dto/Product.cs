﻿namespace DigitalTrade.Catalog.Api.Contracts.Catalog.Dto;

public class Product
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ImageFile { get; set; } = string.Empty;

    public decimal Price { get; set; }
}