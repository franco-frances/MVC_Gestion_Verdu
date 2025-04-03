using Microsoft.EntityFrameworkCore;
using MVC_GestionVerdu.Models;
using MVC_GestionVerdu.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace MVC_GestionVerdu.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly VerduGestionDbContext _context;
        private readonly ILogger<ProductoRepository> _logger;

        public ProductoRepository(VerduGestionDbContext context, ILogger<ProductoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Producto>> GetAllAsync(int usuarioId)
        {
            try
            {
                var productos = await _context.Productos
                    .Where(u => u.UsuarioId == usuarioId)
                    .OrderBy(p => p.Descripcion)
                    .ToListAsync();
                return productos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetAllAsync al obtener los productos para el usuario {UsuarioId}", usuarioId);
                throw new Exception("Ocurrió un error al obtener los productos.");
            }
        }

        public async Task<Producto> GetbyIdAsync(int id)
        {
            try
            {
                var producto = await _context.Productos.FirstOrDefaultAsync(p => p.IdProductos == id);
                return producto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetbyIdAsync al obtener el producto con Id {ProductoId}", id);
                throw new Exception("Ocurrió un error al obtener el producto.");
            }
        }

        public async Task AddAsync(Producto producto)
        {
            try
            {
                await _context.Productos.AddAsync(producto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en AddAsync al agregar el producto");
                throw new Exception("Ocurrió un error al agregar el producto.");
            }
        }

        public async Task UpdateAsync(Producto producto)
        {
            try
            {
                _context.Productos.Update(producto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en UpdateAsync al modificar el producto con Id {ProductoId}", producto.IdProductos);
                throw new Exception("Ocurrió un error al modificar el producto.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var producto = await _context.Productos.FirstOrDefaultAsync(p => p.IdProductos == id);
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en DeleteAsync al eliminar el producto con Id {ProductoId}", id);
                throw new Exception("Ocurrió un error al eliminar el producto.");
            }
        }

        public async Task<bool> ExistsAsync(int usuarioId, string descripcion)
        {
            return await _context.Productos
                .AnyAsync(p => p.UsuarioId == usuarioId && p.Descripcion.ToLower() == descripcion.ToLower());
        }


    }
}
