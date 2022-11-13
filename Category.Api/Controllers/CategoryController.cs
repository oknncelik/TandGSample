using Category.Business.Abstruct;
using Category.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Category.Api.Controllers
{
    /// <summary>
    /// Category Controller
    /// </summary>
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager categoryManager;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="categoryManager"></param>
        public CategoryController(ICategoryManager categoryManager)
        {
            this.categoryManager = categoryManager;
        }

        /// <summary>
        /// Create category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryModel model)
        {
            return Ok(await categoryManager.Create(model));
        }

        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(CategoryModel model)
        {
            return Ok(await categoryManager.Update(model));
        }

        /// <summary>
        /// Delete category by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await categoryManager.Delete(id));
        }

        /// <summary>
        /// Get category by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await categoryManager.Get(id));
        }

        /// <summary>
        /// List of category
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await categoryManager.GetAll());
        }
    }
}
