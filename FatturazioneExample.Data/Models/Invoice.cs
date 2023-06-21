namespace FatturazioneExample.Data.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool HasBeenPaid { get; set; }
        public List<Product> Products { get; set; }
        
        
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}
