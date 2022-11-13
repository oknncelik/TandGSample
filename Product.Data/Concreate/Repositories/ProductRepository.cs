using Product.Data.Abstruct.Repositories;
using Product.Data.Contexts;

namespace Product.Data.Concreate.Repositories
{
    public class ProductRepository : BaseRepository<Entities.Entities.Product>, IProductRepository
    {
        public ProductRepository(ProductContext context) : base(context)
        {
        }
    }
}
