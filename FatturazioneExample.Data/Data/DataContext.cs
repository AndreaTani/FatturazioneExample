using FatturazioneExample.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FatturazioneExample.Data.Data
{
    public class DataContext : DbContext
    {
        // Ogni volta che si fanno modifiche utilizzare quoesto testo tra ||  => | -- "datasource=D:\\DB_SQLite\\sqlite.db"|
        // come parametro finale alle "add migration" ed alle "database update"
        /*
            Ciclo di vita di una modifica
            -----------------------------
            1) Modifica del context
            2) dotnet ef migrations Add MIGRATION_NAME -- "datasource=D:\\DB_SQLite\\sqlite.db"
            3) dotnet ef database update -- "datasource=D:\\DB_SQLite\\sqlite.db"
            4) Profit!
        */


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}
