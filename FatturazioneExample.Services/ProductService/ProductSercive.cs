using FatturazioneExample.Data.Data;
using FatturazioneExample.Data.Models;

namespace FatturazioneExample.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChangesAsync();
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product is null)
            {
                throw new Exception("Product not found!");
            }
            _context.Products.Remove(product);

            _context.SaveChangesAsync();
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                throw new Exception("Product not found!");
            }
            return product;
        }

        public Product UpdateProduct(Product request)
        {
            var product = _context.Products.Find(request.Id);

            if (product == null)
            {
                throw new Exception("Product not found!");
            }

            product.Name = request.Name;
            product.Price = request.Price;

            _context.Products.Update(product);
            _context.SaveChangesAsync();

            return product;
        }
    }
}
