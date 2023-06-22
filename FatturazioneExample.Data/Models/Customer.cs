using System.Text.Json.Serialization;

namespace FatturazioneExample.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletionDate { get; set; }

        [JsonIgnore]
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
