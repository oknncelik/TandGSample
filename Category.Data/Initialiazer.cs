using Category.Data.Abstruct.Repositories;
using Microsoft.Extensions.Hosting;

namespace Category.Data
{
    public class Initialiazer : BackgroundService
    {
        private readonly ICategoryRepository categoryRepository;

        public Initialiazer(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Create Dummy data !
        /// </summary>
        /// <returns></returns>
        public async Task Set()
        {
            var categories = new List<Entities.Entities.Category>
            {
                new Entities.Entities.Category
                {
                    Id = "6370f571dfa8b59f323009e2",
                    Name = "PC"
                },
                new Entities.Entities.Category
                {
                    Id = "6370f585a5a89700615767c4",
                    Name = "Phone"
                },
                new Entities.Entities.Category
                {
                    Id = "6370f5982fa8a03fe867eb74",
                    Name = "Game"
                },
                new Entities.Entities.Category
                {
                    Id = "6370f59cf6a8deead60c62a7",
                    Name = "DVD"
                },
            };

            foreach (var category in categories)
            {
                if(!await categoryRepository.AnyAsync(x=> x.Id == category.Id))
                    await categoryRepository.AddAsync(category);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Set();
            await Task.CompletedTask;
        }
    }
}
