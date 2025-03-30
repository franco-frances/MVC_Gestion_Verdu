using Microsoft.EntityFrameworkCore;
using MVC_GestionVerdu.Models;
using MVC_GestionVerdu.Repositories.Interfaces;
using MVC_GestionVerdu.Services.Interfaces;
using System.Globalization;

namespace MVC_GestionVerdu.Services
{
    public class ReporteServices:IReporteService
    {

        private readonly IReporteRepository _reporteRepository;

        public ReporteServices(IReporteRepository reporteRepository)
        {

            _reporteRepository = reporteRepository;
        }

        public async Task<ReporteViewModel> ObtenerReporteAsync(DateTime? fechaInicio, DateTime? fechaFin, string intervalo)
        {
            fechaInicio ??= DateTime.Today.AddDays(-15);
            fechaFin ??= DateTime.Today;

            // Obtener los ingresos y gastos desde el repositorio
            var ingresosList = await _reporteRepository.ObtenerIngresosAsync(fechaInicio.Value, fechaFin.Value);
            var gastosList = await _reporteRepository.ObtenerGastosAsync(fechaInicio.Value, fechaFin.Value);

            // Función para obtener la clave de agrupación según el intervalo
            Func<DateTime, DateTime> claveAgrupamiento = fecha =>
            {
                if (intervalo.Equals("semana", StringComparison.OrdinalIgnoreCase))
                {
                    return fecha.AddDays(-(int)fecha.DayOfWeek); // Inicio de la semana
                }
                else if (intervalo.Equals("mes", StringComparison.OrdinalIgnoreCase))
                {
                    return new DateTime(fecha.Year, fecha.Month, 1); // Primer día del mes
                }
                else
                {
                    return fecha.Date; // Diario o cualquier otro valor
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

            // Construir y retornar el ReporteViewModel
            return new ReporteViewModel
            {
                FechaInicio = fechaInicio.Value,
                FechaFin = fechaFin.Value,
                Intervalo = intervalo,
                Fechas = todasLasFechas.Select(f =>
                                    intervalo.Equals("semana", StringComparison.OrdinalIgnoreCase)
                                    ? $"del {f.ToString("dd 'de' MMMM", new CultureInfo("es-ES"))} al {f.AddDays(6).ToString("dd 'de' MMMM", new CultureInfo("es-ES"))}"
                                    : intervalo.Equals("mes", StringComparison.OrdinalIgnoreCase)
                                    ? f.ToString("MMMM yyyy", new CultureInfo("es-ES"))
                                    : f.ToString("dd/MM/yyyy")).ToList(),
                Ingresos = todasLasFechas.Select(f => ingresosAgrupados.FirstOrDefault(i => i.Fecha == f)?.Total ?? 0).ToList(),
                Gastos = todasLasFechas.Select(f => gastosAgrupados.FirstOrDefault(g => g.Fecha == f)?.Total ?? 0).ToList(),
                Balance = balance.Select(b => b.Total).ToList()
            };
        }
    }



    }

