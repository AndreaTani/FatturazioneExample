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
        public async Task<ActionResult> AddInvoice(Invoice invoice)
        {
            await _invoiceService.AddInvoice(invoice);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<Invoice>>> GetAllInvoices()
        {
            var invoices = await _invoiceService.GetAllInvoices();
            if (invoices is null)
            {
                return NotFound("Invoices not found!");
            }
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            var invoice = await _invoiceService.GetInvoice(id);
            if (invoice is null)
            {
                return NotFound("Invoice not found!");
            }
            return Ok(invoice);
        }

        [HttpPut]
        public async Task<ActionResult<Invoice>> UpdateInvoice(Invoice request)
        {
            var invoice = await _invoiceService.UpdateInvoice(request);
            if (invoice is null)
            {
                return NotFound("Invoice Not Found!");
            }
            return Ok(invoice);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInvoice(int id)
        {
            await _invoiceService.DeleteInvoice(id);
            return Ok();
        }
    }
}
