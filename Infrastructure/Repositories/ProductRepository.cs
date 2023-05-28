using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;
using Microsoft.Data.Sqlite;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Repositories;

public class ProductRepository : RepositoryBase, IProductRepository
{
    public ProductRepository()
    {
        CreateTables();
        AddData();
    }

    public async Task<int> AddProductAsync(Product product)
    {
        var insertQuery =
            @"
            INSERT INTO Products (Name, Description, LongDescription, Ingredients, Image, Rating, NumRatings, Price)
            VALUES (@name, @description, @longDescription, @ingredients, @image, @rating, @numRatings, @price);
            SELECT last_insert_rowid();"; // get the autoincrement key

        await using var command = new SqliteCommand(insertQuery, Connection);
        command.Parameters.AddWithValue("@name", product.Name);
        command.Parameters.AddWithValue("@description", product.Description);
        command.Parameters.AddWithValue("@longDescription", product.LongDescription);
        command.Parameters.AddWithValue("@ingredients", product.Ingredients);
        command.Parameters.AddWithValue("@image", product.Image);
        command.Parameters.AddWithValue("@rating", product.Rating);
        command.Parameters.AddWithValue("@numRatings", product.NumRatings);
        command.Parameters.AddWithValue("@price", product.Price);

        var result = await command.ExecuteScalarAsync();
        return Convert.ToInt32(result);
    }

    public async Task UpdateProductAsync(Product product)
    {
        var updateQuery = "UPDATE Products SET Name = @name, Description = @description, " +
                          "LongDescription = @longDescription, Ingredients = @ingredients, " +
                          "Image = @image, Rating = @rating, NumRatings = @numRatings, " +
                          "Price = @price WHERE Id = @id";

        await using var command = new SqliteCommand(updateQuery, Connection);
        command.Parameters.AddWithValue("@name", product.Name);
        command.Parameters.AddWithValue("@description", product.Description);
        command.Parameters.AddWithValue("@longDescription", product.LongDescription);
        command.Parameters.AddWithValue("@ingredients", product.Ingredients);
        command.Parameters.AddWithValue("@image", product.Image);
        command.Parameters.AddWithValue("@rating", product.Rating);
        command.Parameters.AddWithValue("@numRating", product.NumRatings);
        command.Parameters.AddWithValue("@price", product.Price);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var deleteQuery = "DELETE FROM Products WHERE Id = @id";

        await using var deleteCommand = new SqliteCommand(deleteQuery, Connection);
        deleteCommand.Parameters.AddWithValue("@id", id);
        await deleteCommand.ExecuteNonQueryAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        var selectQuery = "SELECT * FROM Products WHERE Id = @productId";

        await using var selectCommand = new SqliteCommand(selectQuery, Connection);
        selectCommand.Parameters.AddWithValue("@productId", productId);

        await using var reader = await selectCommand.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            var product = new Product(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetString(5),
                reader.GetInt32(6),
                reader.GetInt32(7),
                reader.GetDecimal(8));

            return product;
        }

        return null; // Return null if the product with the given ID is not found
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        var products = new List<Product>();
        var selectQuery = "SELECT * FROM Products";

        await using var selectCommand = new SqliteCommand(selectQuery, Connection);
        await using var reader = await selectCommand.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var product = new Product(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetString(5),
                reader.GetInt32(6),
                reader.GetInt32(7),
                reader.GetDecimal(8));

            products.Add(product);
        }

        return products;
    }
    
    public async Task BulkInsertProductsAsync(List<Product> products)
    {
        var insertQuery =
            "INSERT INTO Products (Name, Description, LongDescription, Ingredients, Image, Rating, NumRatings, Price) " +
            "VALUES (@name, @description, @longDescription, @ingredients, @image, @rating, @numRatings, @price)";

        foreach (var product in products)
        {
            await using var command = new SqliteCommand(insertQuery, Connection);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@description", product.Description);
            command.Parameters.AddWithValue("@longDescription", product.LongDescription);
            command.Parameters.AddWithValue("@ingredients", product.Ingredients);
            command.Parameters.AddWithValue("@image", product.Image);
            command.Parameters.AddWithValue("@rating", product.Rating);
            command.Parameters.AddWithValue("@numRating", product.NumRatings);
            command.Parameters.AddWithValue("@price", product.Price);

            await command.ExecuteNonQueryAsync();
        }
    }


    protected override void CreateTables()
    {
        var createTableQuery = "CREATE TABLE IF NOT EXISTS Products (" +
                               "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                               "Name TEXT NOT NULL, " +
                               "Description TEXT, " +
                               "LongDescription TEXT, " +
                               "Ingredients TEXT, " +
                               "Image TEXT, " +
                               "Rating INTEGER, " +
                               "NumRatings INTEGER, " +
                               "Price REAL NOT NULL)";

        using var createTableCommand = new SqliteCommand(createTableQuery, Connection);
        createTableCommand.ExecuteNonQuery();
    }

    protected void AddData()
    {
        var addDataQuery =
            "INSERT INTO Products (Name, Description, LongDescription, Ingredients, Image, Rating, NumRatings, Price) " +
            "SELECT * FROM (VALUES " +
            "('Celebrate Health', 'Tomato Sauce 430ml', 'Eating healthy shouldn''t be hard. Real food that''s better for you, even better again if it''s low FODMAP, gluten free and contains no added sugar.', 'Water, Tomato Paste (22%), White Vinegar, Carrot Juice Concentrate, Corn Starch, Salt, Natural Sweetener (Steviol Glycosides), Spices [Cinnamon, Cloves, Nutmeg], Natural Colour [Paprika Extract].', '/images/tomato-sauce.png', 3, 25, 4.99), " +
            "('Nutra Organics', 'Bone Broth Chicken Organic Homestyle Original 125g', 'The Nutra Organics Bone Broth Chicken Organic Homestyle flavour brings you the goodness of nutrient-dense bone broth with the added benefit of a traditional chicken flavour. This health elixir is rich in B Vitamins, Zinc, and gut-loving ingredients that heal the body and the soul while supporting the immune system. It''s easy to use and a versatile kitchen staple. Replacing Nutra Organics Bone Broth Chicken Organic Homestyle Original 100g.', 'Free Range Chicken Bone Broth Powder  (Chicken Bones , Filtered Water, Apple Cider Vinegar , Himalayan Salt) (83%), Roasted Chicken Powder (Roasted Chicken, Tapioca Starch, Ginger, Rosemary), Deactivated Savoury Yeast Flakes, Himalayan Crystal Salt, Ascorbic Acid, Black Pepper. Certified Organic', '/images/chicken-broth.png', 5, 1, 43.95), " +
            "('Ceres Organics', 'Basil Pesto 130g', 'This vegan basil pesto is made with extra virgin olive oil and bursting with basil! It''s cheese-free, 100% certified organic, shelf-stable and made in Italy - the home of pesto.', 'Extra virgin olive oil , Cashew nuts , Genovese Basil P.D.O. in extra virgin olive oil 36% (Genovese Basil P.D.O. 48%, extra virgin olive oil  45%, salt), Apple fibre , Walnuts , Pine kernels , Garlic , Salt ( Certified Organic).', '/images/basil-pesto.png', 5, 6, 8.49), " +
            "('Bae Juice', '120ml', 'Bae Juice is sourced, squeezed and packaged in Naju, South Korea, and brought all the way down under into your hot little hands. Bae literally translates to ''pear'' in Korean, and that�s all Bae Juice is�100% Korean pear juice. The Korean pear is a super-food, and lowers cholesterol, reduces blood pressure, is high in fibre and antioxidants and fill to the brim with micronutrients!', '100% Korean Pear Juice', '/images/bae-juice.png', 4, 20, 3.95), " +
            "('Alter Eco Organic', 'Chocolate Truffles Dark Salted Caramel 108g', 'These Alter Eco Organic Chocolate Dark Salted Caramel Truffles deliver and addictive twist on a classic truffle treat. With a super strict ingredients list featuring only the best in organic ingredients, these truffles are coated in a malty tasting, 58% cocoa Ecuadorian cacao and filled with nourishing coconut oil, and a sprinkling of fleur de sel de Gu�rande for a creamy, deep caramel bite.', 'Organic Raw Cane Sugar, Organic Cacao Beans, Organic Cocoa Butter, Organic Coconut Oil, Organic Whole Milk, Caramel Powder 0.5% (Organic Raw Cane Sugar, Organic Whole Milk), Natural Flavour, Sea Salt (Fleur De Sel) 0.4%, Organic Vanilla Beans.', '/images/dark-chocolate-sc.png', 2, 4, 11.17), " +
            "('Naked Life', 'Nootropics Calm Passionfruit 4x250ml', 'Naked Life''s range of sparkling nootropics uses a unique blend of natural and functional ingredients to deliver an impact you can feel and a taste you''ll love. Designed to bring out your best with every sip, no matter what the day might throw at you.', 'Sparkling Water, Natural Sweeteners (Erythritol, Stevia), Citric Acid, Magnesium Citrate, Natural Extracts (Acerola, Passionflower, Lemon Balm, Ashwagandha Root), Natural Flavours, L-Theanine.', '/images/sparkling-water.png', 1, 16, 10.00)," +
            "('Lum Lum Organic', 'Instant Noodle Chicken 75g', 'Wholegrain brown rice noodles with vegan chicken flavour for a convenient meal ready in minutes.', 'Organic brown rice noodle: brown rice, water organic chicken flavour: organic garlic, organic coriander, organic soybean paste, organic pepper, organic dried onion, organic cane sugar, organic coconut oil, salt, water, citric acid', '/images/instant-noodles.png', 5, 2, 5.79)) AS Temp " +
            "WHERE NOT EXISTS (SELECT 1 FROM Products);";
        using var addDataCommand = new SqliteCommand(addDataQuery, Connection);
        addDataCommand.ExecuteNonQuery();
    }
}