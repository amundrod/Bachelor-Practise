using Bachelor;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add Hangfire services
        services.AddHangfire(config => config.UseStorage(
            new MySqlStorage(_configuration.GetConnectionString("HangfireConnection"),
            new MySqlStorageOptions
            {
                TablePrefix = "Hangfire_"
            })));

        // Add MySQL database context
        services.AddDbContext<PdfDbContext>(options =>
            options.UseMySQL(_configuration.GetConnectionString("PdfConnection")));

        // Add MVC services
        services.AddControllers();
    }
}