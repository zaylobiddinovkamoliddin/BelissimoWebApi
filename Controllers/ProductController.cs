using Belissimo.Dtos.ProductDto;
using BusinessLogicLayer.Helpers;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace Imtihon_Belissimo_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetAllProductAsync();
            return Ok(products);
        }

        [HttpGet("get-products-with-ordetitems")]
        public async Task<IActionResult> GetWithProducts()
        {
            var pro = await _productService.GetAllProductsWithOrderItems();
            return Ok(pro);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(int pageSize, int pageNumber)
        {
            var paged = await _productService.GetPagedProducts(pageSize, pageNumber);

            var metaData = new
            {
                paged.TotalPages,
                paged.PageSize,
                paged.CurrentPage,
                paged.HasPrevious,
                paged.HasNext
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));

            return Ok(paged.Data);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
           var produtc = await _productService.GetProductById(id);
            return Ok(produtc);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddProductDto addProductDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddProductAsync(addProductDto);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductDto updateProductDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(updateProductDto);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok();
        }

        //[HttpGet]
        //public async Task<IActionResult> Filter([FromQuery]FilterParametres filterParametres)
        //{
        //    var products = await _productService.Filter(filterParametres);
        //    return Ok(products);
            
        //}

    }
}
