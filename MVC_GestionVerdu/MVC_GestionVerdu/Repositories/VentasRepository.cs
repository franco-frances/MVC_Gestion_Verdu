using Microsoft.EntityFrameworkCore;
using MVC_GestionVerdu.Models;
using MVC_GestionVerdu.Repositories.Interfaces;

namespace MVC_GestionVerdu.Repositories
{
    public class VentasRepository : IVentasRepository
    {
        private readonly VerduGestionDbContext _context;

        public VentasRepository(VerduGestionDbContext context)
        {

            _context = context;

        }


        public async Task<IEnumerable<DetallesVenta>> GetAllAsync(int usuarioId)
        {
            var ListaVentas = await _context.DetallesVentas
                                            .Include(i => i.MetodoPago)
                                            .Where(v => v.UsuarioId == usuarioId) // Filtrar por UsuarioId
                                            .ToListAsync();

            return ListaVentas;
        }

        public async Task<(IEnumerable<DetallesVenta> ventas, int totalRegistros, decimal totalMonto)> GetVentasPaginadasAsync(int usuarioId, string? metodoPago, DateTime? fechaInicio, DateTime? fechaFin, int pageNumber, int pageSize)
        {

            var query = _context.DetallesVentas.Include(v => v.MetodoPago).Where(v => v.UsuarioId == usuarioId);

            if (!string.IsNullOrEmpty(metodoPago))
                query = query.Where(v => v.MetodoPago.Descripcion == metodoPago);


            // Aplicar filtros de fecha si existen
            if (fechaInicio.HasValue)
                query = query.Where(v => v.Fecha >= fechaInicio.Value);

            if (fechaFin.HasValue)
                query = query.Where(v => v.Fecha <= fechaFin.Value);

            int totalRegistros = await query.CountAsync();

            // Solo calcular totalMonto si se ha filtrado por alguna fecha; de lo contrario, se asigna 0
            decimal totalMonto = 0;
            if (fechaInicio.HasValue || fechaFin.HasValue)
            {
                totalMonto = await query.SumAsync(v => v.Monto);
            }


            var ventas = await query
                               .OrderByDescending(v => v.Fecha)  // Por ejemplo, ordenar por fecha
                               .Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync();

            return (ventas, totalRegistros, totalMonto);

        }


        public async Task<DetallesVenta> GetById(int id)
        {

            var venta = await _context.DetallesVentas.FirstOrDefaultAsync(v => v.IdDetalleVenta == id);
            return venta;



        }


        public async Task<IEnumerable<DetallesVenta>> GetByDayAsync(int usuarioId, DateTime fechaActual)
        {
            var ventas = await _context.DetallesVentas
                                        .Where(v => v.UsuarioId == usuarioId && v.Fecha == fechaActual)
                                        .ToListAsync();
            return ventas;
        }


        public async Task AddAsync(DetallesVenta venta)
        {
            await _context.DetallesVentas.AddAsync(venta);
            await _context.SaveChangesAsync();



        }

        public async Task UpdateAsync(DetallesVenta venta)
        {

            _context.DetallesVentas.Update(venta);
            await _context.SaveChangesAsync();



        }


        public async Task DeleteAsync(int id)
        {

            var venta = await _context.DetallesVentas.FirstOrDefaultAsync(v => v.IdDetalleVenta == id);
            _context.DetallesVentas.Remove(venta);
            await _context.SaveChangesAsync();



        }





    }
}
