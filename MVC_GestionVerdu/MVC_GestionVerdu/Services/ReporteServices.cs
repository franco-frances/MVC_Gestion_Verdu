using Microsoft.EntityFrameworkCore;
using MVC_GestionVerdu.Interfaces;
using MVC_GestionVerdu.Models;
using System.Globalization;

namespace MVC_GestionVerdu.Services
{
    public class ReporteServices:IReporteService
    {

        private readonly VerduGestionDbContext _context;

        public ReporteServices(VerduGestionDbContext context)
        {

            _context = context;
        }

        public async Task<ReporteViewModel> ObtenerReporteAsync(DateTime? fechaInicio, DateTime? fechaFin, string intervalo)
        {
            fechaInicio ??= DateTime.Today.AddDays(-15);
            fechaFin ??= DateTime.Today;

            // Obtener los ingresos y gastos con sus fechas y montos
            var ingresosQuery = _context.DetallesVentas
                .Where(v => v.Fecha >= fechaInicio && v.Fecha <= fechaFin)
                .Select(v => new { Fecha = v.Fecha, Monto = v.Monto });

            var gastosQuery = _context.Gastos
                .Where(g => g.Fecha >= fechaInicio && g.Fecha <= fechaFin)
                .Select(g => new { Fecha = g.Fecha, Monto = g.Monto });

            var ingresosList = await ingresosQuery.ToListAsync();
            var gastosList = await gastosQuery.ToListAsync();

            // Función para obtener la clave de agrupación según el intervalo
            Func<DateTime, DateTime> claveAgrupamiento = fecha =>
            {
                if (intervalo.Equals("semana", StringComparison.OrdinalIgnoreCase))
                {
                    // Inicio de la semana (por ejemplo, domingo)
                    return fecha.AddDays(-(int)fecha.DayOfWeek);
                }
                else if (intervalo.Equals("mes", StringComparison.OrdinalIgnoreCase))
                {
                    // Primer día del mes
                    return new DateTime(fecha.Year, fecha.Month, 1);
                }
                else // "diario" o cualquier otro valor por defecto
                {
                    return fecha.Date;
                }
            };

            // Agrupar ingresos
            var ingresosAgrupados = ingresosList
                .GroupBy(v => claveAgrupamiento(v.Fecha))
                .Select(g => new { Fecha = g.Key, Total = g.Sum(v => v.Monto) })
                .OrderBy(x => x.Fecha)
                .ToList();

            // Agrupar gastos
            var gastosAgrupados = gastosList
                .GroupBy(g => claveAgrupamiento(g.Fecha))
                .Select(g => new { Fecha = g.Key, Total = g.Sum(g => g.Monto) })
                .OrderBy(x => x.Fecha)
                .ToList();

            // Obtener todas las fechas únicas de ambos grupos
            var todasLasFechas = ingresosAgrupados.Select(i => i.Fecha)
                .Union(gastosAgrupados.Select(g => g.Fecha))
                .Distinct()
                .OrderBy(f => f)
                .ToList();

            // Calcular el balance para cada fecha agrupada
            var balance = todasLasFechas
                .Select(f => new
                {
                    Fecha = f.ToString("dd/MM/yyyy"),
                    Total = (ingresosAgrupados.FirstOrDefault(i => i.Fecha == f)?.Total ?? 0) -
                            (gastosAgrupados.FirstOrDefault(g => g.Fecha == f)?.Total ?? 0)
                })
                .ToList();

            var culturaEspanol = new CultureInfo("es-ES");

            // Construir y retornar el ReporteViewModel
            return new ReporteViewModel
            {

                FechaInicio = fechaInicio.Value,
                FechaFin = fechaFin.Value,
                Intervalo= intervalo,
                Fechas = todasLasFechas.Select(f =>
                                intervalo.Equals("semana", StringComparison.OrdinalIgnoreCase)
                                ? $"del {f.ToString("dd 'de' MMMM", culturaEspanol)} al {f.AddDays(6).ToString("dd 'de' MMMM", culturaEspanol)}"
                                : intervalo.Equals("mes", StringComparison.OrdinalIgnoreCase)
                                ? f.ToString("MMMM yyyy", culturaEspanol)
            :                      f.ToString("dd/MM/yyyy")).ToList(),
                Ingresos = todasLasFechas.Select(f => ingresosAgrupados.FirstOrDefault(i => i.Fecha == f)?.Total ?? 0).ToList(),
                Gastos = todasLasFechas.Select(f => gastosAgrupados.FirstOrDefault(g => g.Fecha == f)?.Total ?? 0).ToList(),
                Balance = balance.Select(b => b.Total).ToList()
                
            };
        }



    }
}
