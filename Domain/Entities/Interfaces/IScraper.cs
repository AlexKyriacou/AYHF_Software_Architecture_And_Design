using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

namespace AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;

/// <summary>
/// Defines an interface for scraping products asynchronously.
/// </summary>
public interface IScraper
{
    /// <summary>
    /// Scrapes a list of products asynchronously.
    /// </summary>
    /// <returns>The list of scraped products.</returns>
    Task<List<Product>> ScrapeProductsAsync();
}
