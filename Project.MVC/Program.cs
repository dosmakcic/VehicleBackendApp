using Microsoft.EntityFrameworkCore;
using Ninject;
using Ninject.Extensions.DependencyInjection;
using Project.Service.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


builder.Host.UseServiceProviderFactory(new NinjectServiceProviderFactory());
builder.Host.ConfigureContainer<IKernel>((context, kernel) =>
{
   
    kernel.Load(new NinjectBindings(context.Configuration.GetConnectionString("DefaultConnection")));
});

var app = builder.Build();

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