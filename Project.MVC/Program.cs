using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Service.Data;
using Project.Service.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.MVC;

var builder = WebApplication.CreateBuilder(args);

// Registruj sve servise i konfiguracije
ConfigureServices(builder.Services);

var app = builder.Build();


// TestDatabaseConnection(app.Services); 


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Metod za konfiguraciju servisa u ASP.NET Core DI
void ConfigureServices(IServiceCollection services)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("No connection string found for 'DefaultConnection'.");
    }

    // Dodaj MVC servise
    services.AddControllersWithViews();

    // Dodaj AutoMapper
    services.AddAutoMapper(typeof(MappingProfile).Assembly);

    // Dodaj DbContext
    services.AddDbContext<VehicleContext>(options =>
        options.UseSqlServer(connectionString)
               .EnableSensitiveDataLogging());

    // Dodaj vaše servise
    services.AddTransient<IVehicleService, VehicleService>();
}

/*
void TestDatabaseConnection(IServiceProvider services)
{
    using (var scope = services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<VehicleContext>();

        try
        {
            var vehicleMakes = context.VehicleMakes.ToList();

            Console.WriteLine("Database connection successful!");
            Console.WriteLine("Retrieved Vehicle Makes:");
            foreach (var make in vehicleMakes)
            {
                Console.WriteLine($"- {make.Name} ({make.Abrv})");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Database connection failed:");
            Console.WriteLine(ex.Message);
        }
    }
 
    
}
*/
