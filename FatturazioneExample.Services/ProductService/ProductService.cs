using FatturazioneExample.Data.Data;
using FatturazioneExample.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FatturazioneExample.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                throw new Exception("Product not found!");
            }

            product.IsDeleted = true;
            product.DeletionDate = DateTime.Now;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products
                .Where(p => p.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _context.Products
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                throw new Exception("Product not found!");
            }
            return product;
        }

        public async Task<Product> UpdateProduct(Product request)
        {
            var product = await _context.Products
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(p => p.Id == request.Id);

            if (product == null)
            {
                throw new Exception("Product not found!");
            }

            product.Name = request.Name;
            product.Price = request.Price;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return product;
        }
    }
}
