using Microsoft.EntityFrameworkCore;
using MVC_GestionVerdu.Models;
using MVC_GestionVerdu.Repositories.Interfaces;

namespace MVC_GestionVerdu.Repositories
{
    public class ProductoRepository : IProductoRepository
    {

        private readonly VerduGestionDbContext _context;

        public ProductoRepository(VerduGestionDbContext context)
        {

            _context = context;

        }


        public async Task<IEnumerable<Producto>> GetAllAsync(int usuarioId)
        {
            var productos = await _context.Productos.Where(u => u.UsuarioId == usuarioId).OrderBy(p => p.Descripcion).ToListAsync();
            return productos;
        }

        public async Task<Producto> GetbyIdAsync(int id)
        {

            var Producto = await _context.Productos.FirstOrDefaultAsync(p => p.IdProductos == id);
            return Producto;


        }


        public async Task AddAsync(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Producto producto)
        {

            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();


        }

        public async Task DeleteAsync(int id)
        {

            var Producto = await _context.Productos.FirstOrDefaultAsync(p => p.IdProductos == id);
            _context.Productos.Remove(Producto);
            await _context.SaveChangesAsync();


        }



    }
}
