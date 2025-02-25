using Microsoft.EntityFrameworkCore;
using MVC_GestionVerdu.Interfaces;
using MVC_GestionVerdu.Models;
using MVC_GestionVerdu.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductoService, ProductoServices>();
builder.Services.AddScoped<ICategoriaService, CategoriaServices>();
builder.Services.AddScoped<IUsuarioService, UsuarioServices>();
builder.Services.AddScoped<IVentaService, VentasServices>();
builder.Services.AddScoped<IGastoService, GastosServices>();

builder.Services.AddDbContext<VerduGestionDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));




// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
