
using FatturazioneExample.Data.Data;
using FatturazioneExample.Services.CustomerService;
using FatturazioneExample.Services.ProductService;
using Microsoft.EntityFrameworkCore;

namespace FatturazioneExample.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Injecting ModelServices
            builder.Services.AddTransient<ICustomerService, CustomerService>();
            builder.Services.AddTransient<IProductService, ProductService>();

            // Injecting data context
            var configuration = builder.Configuration;
            var connectionString = configuration.GetConnectionString("SQLiteDB");
            builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(connectionString));
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider; try
                {
                    var context = services.GetRequiredService<DataContext>();
                    context.Database.Migrate();
                    // This will apply the migrations
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}