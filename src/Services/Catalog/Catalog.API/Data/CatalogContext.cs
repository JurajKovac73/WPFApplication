using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
     //   private readonly IConfiguration _configuration;
       // private readonly MongoClient _client;

        public CatalogContext(IConfiguration configuration)
        {
             MongoClient client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
             var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            //  _configuration = configuration;
           Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
