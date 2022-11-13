using AutoMapper;
using Category.Business.Abstruct;
using Category.Data.Abstruct.Repositories;
using Category.Entities.DTOs;

namespace Category.Business.Concreate
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IMapper mapper;
        private readonly ICategoryRepository categoryRepository;

        public CategoryManager(IMapper mapper, ICategoryRepository categoryRepository)
        {
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
        }

        public async Task<IResult> Create(CreateCategoryModel model)
        {
            var entity = mapper.Map<Entities.Entities.Category>(model);

            entity = await categoryRepository.AddAsync(entity);
            if (entity == null)
                return new WarningResult("Category not created !");

            var result = mapper.Map<CategoryModel>(entity);
            return new SuccessResult<CategoryModel>(result);
        }

        public async Task<IResult> Update(CategoryModel model)
        {
            var entity = await categoryRepository.GetByIdAsync(model.Id);
            if (entity == null)
                return new WarningResult("Category not found !");

            entity = mapper.Map<Entities.Entities.Category>(model);
            entity = await categoryRepository.UpdateAsync(entity);
            if (entity == null)
                return new WarningResult("Category not updated !");

            var result = mapper.Map<CategoryModel>(entity);
            return new SuccessResult<CategoryModel>(result);
        }

        public async Task<IResult> Delete(string id)
        {
            var entity = await categoryRepository.GetByIdAsync(id);
            if (entity == null)
                return new WarningResult("Category not found !");

            var result = await categoryRepository.DeleteAsync(entity);
            return new SuccessResult<bool>(result);
        }

        public async Task<IResult> Get(string id)
        {
            var entity = await categoryRepository.GetByIdAsync(id);
            if (entity == null)
                return new WarningResult("Category not found !");

            var result = mapper.Map<CategoryModel>(entity);
            return new SuccessResult<CategoryModel>(result);
        }

        public async Task<IResult> GetAll()
        {
            var entities = await categoryRepository.GetAllAsync(x=> x.ActiveFlag == true);
            if (entities == null)
                return new WarningResult("Categories not found !");

            var result = mapper.Map<List<CategoryModel>>(entities);
            return new SuccessResult<List<CategoryModel>>(result);
        }
    }
}
