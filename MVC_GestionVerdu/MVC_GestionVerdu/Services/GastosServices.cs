using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_GestionVerdu.Interfaces;
using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Services
{
    public class GastosServices:IGastoService
    {

        private readonly VerduGestionDbContext _context;


        public GastosServices(VerduGestionDbContext context)
        {
            _context = context;
            
        }
        public async Task<(IEnumerable<Gastos> gastos, int totalRegistros, decimal totalMonto)> GetGastosPaginados(int usuarioId, DateTime? fechaInicio, DateTime? fechaFin, int pageNumber, int pageSize)
        {
            var query = _context.Gastos.Where(g => g.UsuarioId == usuarioId);

            // Aplicar filtros de fecha si existen
            if (fechaInicio.HasValue)
                query = query.Where(g => g.Fecha >= fechaInicio.Value);

            if (fechaFin.HasValue)
                query = query.Where(g => g.Fecha <= fechaFin.Value);

            int totalRegistros = await query.CountAsync();

            // Solo calcular totalMonto si se ha filtrado por alguna fecha; de lo contrario, se asigna 0
            decimal totalMonto = 0;
            if (fechaInicio.HasValue || fechaFin.HasValue)
            {
                totalMonto = await query.SumAsync(g => g.Monto);
            }

            var gastos = await query
                                .OrderByDescending(g => g.Fecha)  // Por ejemplo, ordenar por fecha
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            return (gastos, totalRegistros, totalMonto);
        }





        public async Task<IEnumerable<Gastos>> GetGastos(int idUsuario)
        {



            var gastos = await _context.Gastos.Where(u=> u.UsuarioId==idUsuario).ToListAsync();
            
            return gastos;


        }

        
        public async Task<Gastos> GetGastoById(int id)
        {

            var gasto = await _context.Gastos.FirstOrDefaultAsync(g => g.IdGasto == id);
            return gasto;


        }

        public async Task<IEnumerable<Gastos>> GetGastosDelDia(int usuarioId, DateTime fechaActual) {

            var gastos = await _context.Gastos
                                .Where(v => v.UsuarioId == usuarioId && v.Fecha == fechaActual)
                                .ToListAsync();
            return gastos;

        }
               

        public async Task AgregarGasto(Gastos gasto)
        {

            await _context.Gastos.AddAsync(gasto);
            await _context.SaveChangesAsync();
            

        }
                

        public async Task EditarGasto(Gastos gasto)
        {


            _context.Gastos.Update(gasto);
            await _context.SaveChangesAsync();
            

        }


        public async Task EliminarGasto(int id)
        {

            var gasto = await _context.Gastos.FirstOrDefaultAsync(g => g.IdGasto == id);
            _context.Gastos.Remove(gasto);
            await _context.SaveChangesAsync();
           

        }

    }
}
