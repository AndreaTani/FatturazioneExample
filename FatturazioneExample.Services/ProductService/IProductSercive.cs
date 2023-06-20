using FatturazioneExample.Data.Models;

namespace FatturazioneExample.Services.ProductService
{
    public interface IProductService
    {
        void AddProduct(Product product);
        List<Product> GetAllProducts();
        Product GetProduct(int id);
        Product UpdateProduct(Product request);
        void DeleteProduct(int id);
    }
}
