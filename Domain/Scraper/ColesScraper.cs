using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using HtmlAgilityPack;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Scrapers
{
    public class ColesScraper : IScraper
    {
        public async Task<List<Product>> ScrapeProductsAsync()
        {
            var products = new List<Product>();
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync("https://www.coles.com.au/shop/browse/fruit-veg");

            var productNodes = doc.DocumentNode.SelectNodes("//section[contains(@class, 'coles-targeting-ProductTileProductTileWrapper')]");

            foreach (var productNode in productNodes)
            {
                var name = productNode.SelectSingleNode(".//a[@class='product-title']").InnerText;
                var image = productNode.SelectSingleNode(".//img").GetAttributeValue("src", "");
                var price = decimal.Parse(productNode.SelectSingleNode(".//span[@class='price-dollars']").InnerText.Replace("$", ""));
                var ratingNode = productNode.SelectSingleNode(".//div[@class='stars']");
                var rating = ratingNode != null ? int.Parse(ratingNode.GetAttributeValue("aria-label", "").Replace("stars", "")) : 0;
                var numRatingsNode = productNode.SelectSingleNode(".//span[@class='ratings']");
                var numRatings = numRatingsNode != null ? int.Parse(numRatingsNode.InnerText) : 0;

                var product = new Product(0, name, "", "", "", image, rating, numRatings, price);
                products.Add(product);
            }

            return products;
        }
    }
}