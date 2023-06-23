using FatturazioneExample.Data.Models;
using FatturazioneExample.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace FatturazioneExample.API.Controllers
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

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            await _productService.AddProduct(product);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            if (products is null)
            {
                return NotFound("Products not found");
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProduct(id);
            if (product is null)
            {
                return NotFound("Product not found!");
            }
            return Ok(product);
        }

        [HttpPut]
        public async Task<ActionResult<Product>> UpdateProduct(Product request)
        {
            var product = await _productService.UpdateProduct(request);
            if (product == null)
            {
                return NotFound("Product not found!");
            }

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProduct(id);
            return Ok();
        }
    }
}
