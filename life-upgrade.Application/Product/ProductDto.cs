using System.ComponentModel.DataAnnotations;
using LifeUpgrade.Domain.Entities;

namespace LifeUpgrade.Application.Product;

public class ProductDto
{
    public string Name { get; set; } = default!;
    public Uri Uri { get; set; } = default!;
    public decimal Price { get; set; }
    public List<ProductType> Type { get; set; } = [];
    public string? EncodedName { get; set; }
}