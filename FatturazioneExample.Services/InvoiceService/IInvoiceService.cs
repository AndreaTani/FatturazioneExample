using FatturazioneExample.Data.Models;

namespace FatturazioneExample.Services.InvoiceService
{
    public interface IInvoiceService
    {
        void AddInvoice(Invoice invoice);
        List<Invoice> GetAllInvoices();
        Invoice GetInvoice(int id);
        Invoice UpdateInvoice(Invoice invoice);
        void DeleteInvoice(int id);
    }
}
