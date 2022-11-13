using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Business.Abstruct;
using Product.Entities.DTOs;

namespace Product.Api.Controllers
{
    /// <summary>
    /// Product Controller
    /// </summary>
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager productManager;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="productManager"></param>
        public ProductController(IProductManager productManager)
        {
            this.productManager = productManager;
        }

        /// <summary>
        /// Create product
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductModel model)
        {
            return Ok(await productManager.Create(model));
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(ProductModel model)
        {
            return Ok(await productManager.Update(model));
        }

        /// <summary>
        /// Delete product by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            return Ok(await productManager.Delete(id));
        }

        /// <summary>
        /// Get product by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            return Ok(await productManager.Get(id));
        }

        /// <summary>
        /// List of product
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await productManager.GetAll());
        }

        /// <summary>
        /// category product list
        /// </summary>
        /// <returns></returns>
        [HttpGet("categoryproduct/{category}")]
        public async Task<IActionResult> CategoryProduct(string category)
        {
            return Ok(await productManager.CategoryProduct(category));
        }
    }
}
