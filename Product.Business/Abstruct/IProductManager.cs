using Product.Entities.DTOs;

namespace Product.Business.Abstruct
{
    public interface IProductManager
    {
        Task<IResult> CategoryProduct(string category);
        Task<IResult> Create(CreateProductModel model);
        Task<IResult> Delete(long id);
        Task<IResult> Get(long id);
        Task<IResult> GetAll();
        Task<IResult> Update(ProductModel model);
    }
}
