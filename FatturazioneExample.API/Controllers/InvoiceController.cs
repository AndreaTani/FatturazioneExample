using FatturazioneExample.Data.Models;
using FatturazioneExample.Services.InvoiceService;
using Microsoft.AspNetCore.Mvc;

namespace FatturazioneExample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost]
        public ActionResult AddInvoice(Invoice invoice)
        {
            _invoiceService.AddInvoice(invoice);
            return Ok();
        }

        [HttpGet]
        public ActionResult<List<Invoice>> GetAllInvoices()
        {
            var invoices = _invoiceService.GetAllInvoices();
            if (invoices is null)
            {
                return NotFound("Invoices not found!");
            }
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public ActionResult<Invoice> GetInvoice(int id)
        {
            var invoice = _invoiceService.GetInvoice(id);
            if (invoice is null)
            {
                return NotFound("Invoice not found!");
            }
            return Ok(invoice);
        }

        [HttpPut]
        public ActionResult<Invoice> UpdateInvoice(Invoice request)
        {
            var invoice = _invoiceService.UpdateInvoice(request);
            if (invoice is null)
            {
                return NotFound("Invoice Not Found!");
            }
            return Ok(invoice);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteInvoice(int id)
        {
            _invoiceService.DeleteInvoice(id);
            return Ok();
        }
    }
}
