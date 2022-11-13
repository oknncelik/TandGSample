using Microsoft.Extensions.Hosting;
using Product.Data.Abstruct.Repositories;

namespace Product.Data
{
    public class Initialiazer : BackgroundService
    {
        private readonly IProductRepository productRepository;

        public Initialiazer(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        /// <summary>
        /// Create Dummy data !
        /// </summary>
        /// <returns></returns>
        public async Task Set()
        {
            var products = new List<Entities.Entities.Product>();


            for (int i = 1; i < new Random().Next(5, 15); i++)
            {
                products.Add(new Entities.Entities.Product
                {
                    Category = "6370f571dfa8b59f323009e2",
                    Name = $"PC Product {i}",
                    Description = $"PC Product {i}",
                    Quantity = new Random().Next(1, 250),
                    Price = new Random().Next(1, 250) * 100

                });
            }

            for (int i = 1; i < new Random().Next(5, 15); i++)
            {
                products.Add(new Entities.Entities.Product
                {
                    Category = "6370f585a5a89700615767c4",
                    Name = $"Phone Product {i}",
                    Description = $"Phone Product {i}",
                    Quantity = new Random().Next(1, 250),
                    Price = new Random().Next(1, 250) * 100
                });
            }

            for (int i = 1; i < new Random().Next(5, 15); i++)
            {
                products.Add(new Entities.Entities.Product
                {
                    Category = "6370f5982fa8a03fe867eb74",
                    Name = $"Game Product {i}",
                    Description = $"Game Product {i}",
                    Quantity = new Random().Next(1, 250),
                    Price = new Random().Next(1, 250) * 100
                });
            }


            for (int i = 1; i < new Random().Next(5, 15); i++)
            {
                products.Add(new Entities.Entities.Product
                {
                    Category = "6370f59cf6a8deead60c62a7",
                    Name = $"DVD Product {i}",
                    Description = $"DVD Product {i}",
                    Quantity = new Random().Next(1, 250),
                    Price = new Random().Next(1, 250) * 100
                });
            }

            foreach (var product in products)
            {
                if (!await productRepository.AnyAsync(x => x.Id == product.Id))
                    await productRepository.AddAsync(product);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Set();
            await Task.CompletedTask;
        }
    }
}
