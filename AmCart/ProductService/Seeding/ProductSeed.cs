using MongoDB.Bson;
using System.Reflection.PortableExecutable;

namespace ProductService.Seeding
{
    public static class ProductSeed
    {
        public static List<object> products = [ new {
            Id = ObjectId.GenerateNewId().ToString(),
            ProductTag = "SMRT_123_1",
            Name= "Men Blue Slim Fit Jeans",
            Description= "Modern and trendy slim fit jeans for an urban lifestyle and also Timeless and durable denim jeans for a stylish look.",
            Price= 859,
            SalePrice= 499,
            StockQuantity= 100,
            CategoryPath= "/Men/Clothing/Jeans",
            Category= "Men",
            Subcategory= "Jeans ",
            Brand= "Levi’s",
            Color= "Blue",
            Rating= 4.2,
            discount= new {
            flatDiscount= false,
            flatDiscountCurrency= "INR",
            percentageDiscount= "50"
            },
        }];
    }
}
