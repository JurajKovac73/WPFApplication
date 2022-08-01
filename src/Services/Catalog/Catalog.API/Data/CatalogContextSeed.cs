using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();

            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            IEnumerable<Product> products = new List<Product>()
                {
                new Product(){
                Id="602d2149e773f2a3990b47f1",
                Name = "Acer Laptop",
                Category= "Computers",
                Summary= "Summary",
                Description= "Description",
                    ImageFile= "ImageFile1.png",
                Price= 900.53M
                },
                new Product(){
                Id="602d2149e773f2a3990b47f2",
                Name = "Toshiba Laptop",
                Category= "Computers",
                Summary= "Trvacny",
                Description= "Description",
                    ImageFile= "ImageFile2.png",
                Price= 1000.53M
                },
                 new Product(){
                Id="602d2149e773f2a3990b47f3",
                Name = "Asus Laptop",
                Category= "Computers",
                Summary= "Summary",
                Description= "Description",
                    ImageFile= "ImageFile3.png",
                Price= 1500.27M
                },
                new Product(){
                Id="602d2149e773f2a3990b47f4",
                Name = "Predator Laptop",
                Category= "Computers",
                Summary= "Summary",
                Description= "super pocitac",
                    ImageFile= "ImageFile4.png",
                Price= 2000.35M
                },
                 new Product(){
                Id="602d2149e773f2a3990b47f5",
                Name = "Xiaomi",
                Category= "Smart Phone",
                Summary= "Summary",
                Description= "Description",
                    ImageFile= "ImageFile5.png",
                Price= 320.53M
                },
                new Product(){
                Id="602d2149e773f2a3990b47f6",
                Name = "Nokia",
                Category= "Smart Phone",
                Summary= "Summary",
                Description= "Description",
                    ImageFile= "ImageFile6.png",
                Price= 500.53M
                },
                 new Product(){
                Id="602d2149e773f2a3990b47f7",
                Name = "Led podsvietena",
                Category= "Keyboard",
                Summary= "Summary",
                Description= "Description",
                    ImageFile= "ImageFile7.png",
                Price= 20.53M
                },
                new Product(){
                Id="602d2149e773f2a3990b47f8",
                Name = "Bez podsvietenia",
                Category= "Keyboard",
                Summary= "Summary",
                Description= "Description",
                    ImageFile= "ImageFile8.png",
                Price= 10.53M
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47g5",
                    Name = "IPhone X",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-1.png",
                    Price = 950.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47g6",
                    Name = "Nokia 10",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-2.png",
                    Price = 840.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47g7",
                    Name = "Huawei Plus",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-3.png",
                    Price = 650.00M,
                    Category = "White Appliances"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47g8",
                    Name = "Xiaomi Mi 9",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-4.png",
                    Price = 470.00M,
                    Category = "White Appliances"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47g9",
                    Name = "HTC U11+ Plus",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-5.png",
                    Price = 380.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47ga",
                    Name = "LG G7 ThinQ",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-6.png",
                    Price = 240.00M,
                    Category = "Home Kitchen"
                }
                };

            return products;
        }
    }
}
