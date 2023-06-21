using System.Text.Json.Serialization;

namespace FatturazioneExample.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

        [JsonIgnore]
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
