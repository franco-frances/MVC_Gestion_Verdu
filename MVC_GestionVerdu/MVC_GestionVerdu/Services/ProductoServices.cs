using MVC_GestionVerdu.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MVC_GestionVerdu.Interfaces;

namespace MVC_GestionVerdu.Services
{

    


    public class ProductoServices:IProductoService
    {

        private readonly VerduGestionDbContext _context;

        public ProductoServices(VerduGestionDbContext context)
        {
           
            _context = context;

        }


        public async Task<IEnumerable<Producto>> ListarProductos()
        {
            var productos = await _context.Productos.ToListAsync();
            return productos;
        }


        public async Task<Producto> GetProducto(int id)
        {

            var Producto = await _context.Productos.FirstOrDefaultAsync(p => p.IdProductos == id);
            return Producto;


        }

        public async Task AgregarProducto(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
        }


        public async Task EditarProducto(Producto producto)
        {

            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
            

        }

        public async Task EliminarProducto(int id)
        {

            var Producto = await _context.Productos.FirstOrDefaultAsync(p => p.IdProductos == id);
            _context.Productos.Remove(Producto);
            await _context.SaveChangesAsync();
            

        }



    }
}
