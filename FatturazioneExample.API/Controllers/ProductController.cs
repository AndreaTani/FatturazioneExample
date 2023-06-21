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
        public ActionResult AddProduct(Product product)
        {
            _productService.AddProduct(product);
            return Ok();
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            if (products is null)
            {
                return NotFound("Products not found");
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _productService.GetProduct(id);
            if (product is null)
            {
                return NotFound("Product not found!");
            }
            return Ok(product);
        }

        [HttpPut]
        public ActionResult<Product> UpdateProduct(Product request)
        {
            var product = _productService.UpdateProduct(request);
            if (product == null)
            {
                return NotFound("Product not found!");
            }

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            return Ok();
        }
    }
}
