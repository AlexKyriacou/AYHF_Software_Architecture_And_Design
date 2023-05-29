using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using HtmlAgilityPack;

namespace AYHF_Software_Architecture_And_Design.Domain.Scraper;

public class WoolworthsScraper : IScraper
{
    public async Task<List<Product>> ScrapeProductsAsync()
    {
        var products = new List<Product>();
        var web = new HtmlWeb();
        var doc = await web.LoadFromWebAsync("https://www.woolworths.com.au/shop/browse/fruit-veg");

        var productNodes = doc.DocumentNode.SelectNodes("//section[@class='product-tile-v2']");

        foreach (var productNode in productNodes)
        {
            var name = productNode.SelectSingleNode(".//a[@class='product-title-link ng-star-inserted']").InnerText;
            var image = productNode.SelectSingleNode(".//img").GetAttributeValue("src", "");
            var price = decimal.Parse(productNode.SelectSingleNode(".//div[@class='primary']").InnerText
                .Replace("$", ""));
            var ratingNode = productNode.SelectSingleNode(".//span[@class='rating-content']");
            var rating = ratingNode != null ? int.Parse(ratingNode.InnerText) : 0;
            var numRatingsNode = productNode.SelectSingleNode(".//a[@href]");
            var numRatings = numRatingsNode != null ? int.Parse(numRatingsNode.InnerText) : 0;

            var product = new Product(0, name, "", "", "", image, rating, numRatings, price);
            products.Add(product);
        }

        return products;
    }
}