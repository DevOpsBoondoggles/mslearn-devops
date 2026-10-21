using CatalogApi.Models;
using CatalogApi.Services;
using Xunit;

public class ProductSearchTests
{
    private static List<Product> Sample() => new()
    {
        new() { Name = "A", Stock = 0 },
        new() { Name = "B", Stock = 5 },
        new() { Name = "C", Stock = 0 },
        new() { Name = "D", Stock = 1 },
    };

    [Fact]
    public void OutOfStockItemsAreSortedLast_PreservingOrder()
    {
        var names = ProductSearch.Apply(Sample(), false).Select(p => p.Name);
        Assert.Equal(new[] { "B", "D", "A", "C" }, names);
    }

    [Fact]
    public void HideOutOfStock_RemovesOutOfStockItems()
    {
        var result = ProductSearch.Apply(Sample(), true).ToList();
        Assert.Equal(2, result.Count);
        Assert.All(result, p => Assert.False(p.IsOutOfStock));
    }

    [Fact]
    public void Badge_OnlyForOutOfStock()
    {
        Assert.Equal("Out of Stock", new Product { Stock = 0 }.Badge);
        Assert.Null(new Product { Stock = 3 }.Badge);
    }
}
