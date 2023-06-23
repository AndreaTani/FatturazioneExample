using FatturazioneExample.Data.Models;

namespace FatturazioneExample.Services.InvoiceService
{
    public interface IInvoiceService
    {
        Task AddInvoice(Invoice invoice);
        Task<List<Invoice>> GetAllInvoices();
        Task<Invoice> GetInvoice(int id);
        Task<Invoice> UpdateInvoice(Invoice invoice);
        Task DeleteInvoice(int id);
    }
}
