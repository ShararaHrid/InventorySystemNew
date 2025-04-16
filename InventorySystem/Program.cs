using Autofac;
using Autofac.Extensions.DependencyInjection;
using InventorySystem.Data;
using InventorySystem.Repositories;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// 🔸 Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// 🔹 Use Autofac as the DI Container
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    // Register your services here for Autofac
    containerBuilder.RegisterType<UnitOfWork>().AsSelf().InstancePerLifetimeScope();
    containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
});

// 🔸 Configure Entity Framework Core (EF Core) for MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("InventoryDB")));

// 🔸 Add services to the container
builder.Services.AddControllersWithViews();

// No need to register UnitOfWork again using AddScoped if you're using Autofac

var app = builder.Build();

// 🔸 Configure Middleware Pipeline
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
