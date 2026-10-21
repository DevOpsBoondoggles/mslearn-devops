using CatalogApi.Models;

namespace CatalogApi.Services;

public static class ProductSearch
{
    public static IEnumerable<Product> Apply(IEnumerable<Product> products, bool hideOutOfStock)
    {
        var items = hideOutOfStock ? products.Where(p => !p.IsOutOfStock) : products;
        return items.OrderBy(p => p.IsOutOfStock).ToList();
    }
}
