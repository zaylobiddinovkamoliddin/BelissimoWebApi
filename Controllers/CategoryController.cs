using Belissimo.Dtos.CategoryDtos;
using BusinessLogicLayer.Helpers;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Imtihon_Belissimo_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var category = await _categoryService.GatCategoriesAsync();
            return Ok(category);
        }


        [HttpGet("get-with-products")]
        public async Task<IActionResult> GetWithProduct()
        {
            var category = await _categoryService.GetAllWithProduct();
            return Ok(category);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(int pageSize, int pageNumber)
        {
            var paged = await _categoryService.GetPagetCategories(pageSize, pageNumber);
            var metaData = new
            {
                paged.TotalCount,
                paged.PageSize,
                paged.CurrentPage,
                paged.HasNext,
                paged.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));
            return Ok(paged.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddCategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddCategoryAsync(categoryDto);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateCategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCatgoryAsync(categoryDto);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok();
        }
    }
}
