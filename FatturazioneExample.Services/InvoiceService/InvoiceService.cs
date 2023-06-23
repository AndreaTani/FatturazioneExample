using FatturazioneExample.Data.Data;
using FatturazioneExample.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FatturazioneExample.Services.InvoiceService
{
    public class InvoiceService : IInvoiceService
    {
        private readonly DataContext _context;

        public InvoiceService(DataContext context)
        {
            _context = context;
        }

        public async Task AddInvoice(Invoice invoice)
        {
            var customerId = invoice.Customer.Id;
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer is not null)
            {
                invoice.Customer = customer;
            }

            var products = invoice.Products;
            var allProducts = await _context.Products.ToListAsync();
            var existingProducts = _context.Products.Where(p => allProducts.Contains(p));

            for (int i = 0; i < invoice.Products.Count; i++)
            {
                var existingProduct = existingProducts.FirstOrDefault(p => p.Id == invoice.Products[i].Id);
                if (existingProduct is not null)
                {
                    invoice.Products[i] = existingProduct;
                }
            }

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInvoice(int id)
        {
            var invoice = await _context.Invoices
                .Where(i => i.IsDeleted == false)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice is null)
            {
                throw new Exception("Invoice not found!");
            }

            invoice.IsDeleted = true;
            invoice.DeletionDate = DateTime.Now;

            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Invoice>> GetAllInvoices()
        {
            return await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Products)
                .Where(i => i.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<Invoice> GetInvoice(int id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Products)
                .Where(i => i.IsDeleted == false)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (invoice is null)
            {
                throw new Exception("Invoice not found!");
            }
            return invoice;
        }

        public async Task<Invoice> UpdateInvoice(Invoice request)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Products)
                .Where(i => i.IsDeleted == false)
                .FirstOrDefaultAsync(i => i.Id == request.Id);
            if (invoice is null)
            {
                throw new Exception("Invoice not found!");
            }

            // customer of an invoice cannot be changed
            if (request.CustomerId != invoice.CustomerId || request.Customer.Id != invoice.Customer.Id)
            {
                throw new Exception("Customer is invalid");
            }

            // Retrieve product Ids
            var productIds = request.Products.Select(p => p.Id).ToList();

            // Retrieve the existing products associated with the invoice
            var existingProducts = await _context.Products.Where(p => invoice.Products.Contains(p)).ToListAsync();

            // Check if the request contains any new products
            var newProductIds = productIds.Except(existingProducts.Select(p => p.Id)).ToList();
            var newProducts = await _context.Products.Where(p => newProductIds.Contains(p.Id)).ToListAsync();

            // Check if any existing products are removed
            var removedProducts = existingProducts.Where(p => !productIds.Contains(p.Id)).ToList();

            // Add new products
            foreach (var newProduct in newProducts)
            {
                invoice.Products.Add(newProduct);
            }

            // Remove removed products
            foreach (var removedProduct in removedProducts)
            {
                invoice.Products.Remove(removedProduct);
            }


            invoice.Date = request.Date;
            invoice.HasBeenPaid = request.HasBeenPaid;

            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync();

            return invoice;
        }
    }
}
