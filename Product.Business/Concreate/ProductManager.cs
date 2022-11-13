using AutoMapper;
using Product.Business.Abstruct;
using Product.Data.Abstruct.Repositories;
using Product.Entities.DTOs;

namespace Product.Business.Concreate
{
    public class ProductManager : IProductManager
    {
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;

        public ProductManager(IMapper mapper, IProductRepository productRepository)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }

        public async Task<IResult> Create(CreateProductModel model)
        {
            var entity = mapper.Map<Entities.Entities.Product>(model);

            entity = await productRepository.AddAsync(entity);
            if (entity == null)
                return new WarningResult("Product not created !");

            var result = mapper.Map<ProductModel>(entity);
            return new SuccessResult<ProductModel>(result);
        }

        public async Task<IResult> Update(ProductModel model)
        {
            var entity = await productRepository.GetByIdAsync(model.Id);
            if (entity == null)
                return new WarningResult("Product not found !");

            entity = mapper.Map<Entities.Entities.Product>(model);
            entity = await productRepository.UpdateAsync(entity);
            if (entity == null)
                return new WarningResult("Product not updated !");

            var result = mapper.Map<ProductModel>(entity);
            return new SuccessResult<ProductModel>(result);
        }

        public async Task<IResult> Delete(long id)
        {
            var entity = await productRepository.GetByIdAsync(id);
            if (entity == null)
                return new WarningResult("Product not found !");

            var result = await productRepository.DeleteAsync(entity);
            return new SuccessResult<bool>(result);
        }

        public async Task<IResult> Get(long id)
        {
            var entity = await productRepository.GetByIdAsync(id);
            if (entity == null)
                return new WarningResult("Product not found !");

            var result = mapper.Map<ProductModel>(entity);
            return new SuccessResult<ProductModel>(result);
        }

        public async Task<IResult> GetAll()
        {
            var entities = await productRepository.GetAllAsync(x => true);
            if (entities == null)
                return new WarningResult("Products not found !");

            var result = mapper.Map<List<ProductModel>>(entities);
            return new SuccessResult<List<ProductModel>>(result);
        }

        public async Task<IResult> CategoryProduct(string category)
        {
            var entities = await productRepository.GetAllAsync(x => x.Category == category);
            if (entities == null)
                return new WarningResult("Products not found !");

            var result = mapper.Map<List<ProductModel>>(entities);
            return new SuccessResult<List<ProductModel>>(result);
        }
    }
}
