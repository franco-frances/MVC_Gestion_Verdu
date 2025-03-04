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
builder.Services.AddScoped<IAuthService, AuthServices>();
builder.Services.AddScoped<IMetodoPagoService,MetodoPagosServices>();

builder.Services.AddDbContext<VerduGestionDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));


// Agregar servicios para la sesión
builder.Services.AddDistributedMemoryCache(); // Almacén en memoria para la sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true; // Seguridad contra scripts
    options.Cookie.IsEssential = true; // Necesario para la funcionalidad esencial
});




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


app.UseSession(); // Importante: Esto debe estar antes de `app.UseAuthorization()`

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
