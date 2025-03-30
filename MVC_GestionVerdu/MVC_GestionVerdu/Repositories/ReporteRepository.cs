using MVC_GestionVerdu.Models;
using Microsoft.EntityFrameworkCore;
using MVC_GestionVerdu.Repositories.Interfaces;

namespace MVC_GestionVerdu.Repositories
{
    public class ReporteRepository : IReporteRepository
    {

        private readonly VerduGestionDbContext _context;

        public ReporteRepository(VerduGestionDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DetallesVenta>> ObtenerIngresosAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.DetallesVentas
                .Where(v => v.Fecha >= fechaInicio && v.Fecha <= fechaFin)
                .Select(v => new DetallesVenta { Fecha = v.Fecha, Monto = v.Monto })
            .ToListAsync();
        }

        public async Task<IEnumerable<Gastos>> ObtenerGastosAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Gastos
                .Where(g => g.Fecha >= fechaInicio && g.Fecha <= fechaFin)
                .Select(g => new Gastos { Fecha = g.Fecha, Monto = g.Monto })
                .ToListAsync();
        }


    }
}
