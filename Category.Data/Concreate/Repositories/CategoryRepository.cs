using Category.Common.Configs.Models;
using Category.Data.Abstruct.Repositories;
using Category.Data.MongoDb.Concreate;

namespace Category.Data.Concreate.Repositories
{
    public class CategoryRepository : BaseRepository<Entities.Entities.Category>, ICategoryRepository
    {
        public CategoryRepository(MongoDbSettings setting) : base(setting)
        {
        }
    }
}
