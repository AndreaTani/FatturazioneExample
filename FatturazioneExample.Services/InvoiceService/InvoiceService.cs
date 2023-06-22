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
        public void AddInvoice(Invoice invoice)
        {
            var customerId = invoice.Customer.Id;
            var customer = _context.Customers.Find(customerId);
            if (customer is not null)
            {
                invoice.Customer = customer;
            }

            var products = invoice.Products;
            var existingProducts = _context.Products.Where(p => products.Contains(p));

            for (int i = 0; i < invoice.Products.Count; i++)
            {
                var existingProduct = existingProducts.FirstOrDefault(p => p.Id == invoice.Products[i].Id);
                if (existingProduct is not null)
                {
                    invoice.Products[i] = existingProduct;
                }
            }

            _context.Invoices.Add(invoice);
            _context.SaveChanges();
        }

        public void DeleteInvoice(int id)
        {
            var invoice = _context.Invoices
                .Where(i => i.IsDeleted == false)
                .FirstOrDefault(i => i.Id == id);

            if (invoice is null)
            {
                throw new Exception("Invoice not found!");
            }

            invoice.IsDeleted = true;
            invoice.DeletionDate = DateTime.Now;

            _context.Invoices.Update(invoice);
            _context.SaveChanges();
        }

        public List<Invoice> GetAllInvoices()
        {
            return _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Products)
                .Where(i => i.IsDeleted == false)
                .ToList();
        }

        public Invoice GetInvoice(int id)
        {
            var invoice = _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Products)
                .Where(i => i.IsDeleted == false)
                .FirstOrDefault(i => i.Id == id);
            if (invoice is null)
            {
                throw new Exception("Invoice not found!");
            }
            return invoice;
        }

        public Invoice UpdateInvoice(Invoice request)
        {
            var invoice = _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Products)
                .Where(i => i.IsDeleted == false)
                .FirstOrDefault(i => i.Id == request.Id);
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
            var existingProducts = _context.Products.Where(p => invoice.Products.Contains(p)).ToList();

            // Check if the request contains any new products
            var newProductIds = productIds.Except(existingProducts.Select(p => p.Id)).ToList();
            var newProducts = _context.Products.Where(p => newProductIds.Contains(p.Id)).ToList();

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
            _context.SaveChanges();

            return invoice;
        }
    }
}
