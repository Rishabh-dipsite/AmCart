using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using ProductService.Seeding;
using System.Collections;
using System.Text.Json;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<object> _productCollection;
        private readonly IDistributedCache _cache;
        private readonly TimeSpan _cacheExpiry = TimeSpan.FromMinutes(5);

        public ProductController(IMongoClient mongoClient, IConfiguration configuration, ILogger<ProductController> logger, IDistributedCache cache)
        {
            var databaseName = configuration.GetValue<string>("MongoDbSettings:DatabaseName");
            _database = mongoClient.GetDatabase(databaseName);
            var collectionName = configuration.GetValue<string>("MongoDbSettings:Collections:ProductCollection");
            //_database.DropCollection(collectionName);
            _productCollection = _database.GetCollection<object>(collectionName);
            //_productCollection.DeleteMany(Builders<object>.Filter.Eq(p => 1, 1));
            _logger = logger;
            _cache = cache;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create()
        {
            try
            {
                //await _productCollection.InsertManyAsync(ProductSeed.products);
                await _productCollection.InsertOneAsync(ProductSeed.products[0]);


                string cacheKey = "Item_SMRT_123_1";
                // Cache the item in Redis
                await _cache.SetStringAsync(
                    cacheKey,
                    JsonSerializer.Serialize(ProductSeed.products[0]),
                    new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = _cacheExpiry }
                );
                return Ok();
            }
            catch (MongoWriteException ex)
            {
                return BadRequest($"Failed to insert document: {ex.Message}");
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productCollection.Find(Builders<object>.Filter.Empty).ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            //var item = await _itemService.GetItemAsync(id);

            string cacheKey = $"Item_{id}";

            // Step 1: Try to get the item from Redis cache
            var cachedItem = await _cache.GetStringAsync(cacheKey);
            if (!String.IsNullOrWhiteSpace(cachedItem))
                return Ok(JsonSerializer.Deserialize<dynamic>(cachedItem));
            else
                return NoContent();
        }
    }
}
