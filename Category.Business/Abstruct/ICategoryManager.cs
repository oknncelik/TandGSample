using Category.Entities.DTOs;

namespace Category.Business.Abstruct
{
    public interface ICategoryManager
    {
        Task<IResult> Create(CreateCategoryModel model);
        Task<IResult> Delete(string id);
        Task<IResult> Update(CategoryModel model);
        Task<IResult> Get(string id);
        Task<IResult> GetAll();
    }
}
