using FatturazioneExample.Data.Models;

namespace FatturazioneExample.Services.ProductService
{
    public interface IProductService
    {
        Task AddProduct(Product product);
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProduct(int id);
        Task<Product> UpdateProduct(Product request);
        Task DeleteProduct(int id);
    }
}
