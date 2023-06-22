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
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products
                .Where(p => p.IsDeleted == false)
                .FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
                throw new Exception("Product not found!");
            }

            product.IsDeleted = true;
            product.DeletionDate = DateTime.Now;

            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products
                .Where(p => p.IsDeleted == false)
                .ToList();
        }

        public Product GetProduct(int id)
        {
            var product = _context.Products
                .Where(p => p.IsDeleted == false)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                throw new Exception("Product not found!");
            }
            return product;
        }

        public Product UpdateProduct(Product request)
        {
            var product = _context.Products
                .Where(p => p.IsDeleted == false)
                .FirstOrDefault(p => p.Id == request.Id);

            if (product == null)
            {
                throw new Exception("Product not found!");
            }

            product.Name = request.Name;
            product.Price = request.Price;

            _context.Products.Update(product);
            _context.SaveChanges();

            return product;
        }
    }
}
