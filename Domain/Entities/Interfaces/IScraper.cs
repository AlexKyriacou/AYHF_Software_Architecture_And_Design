using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

namespace AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;

public interface IScraper
{
    Task<List<Product>> ScrapeProductsAsync();
}