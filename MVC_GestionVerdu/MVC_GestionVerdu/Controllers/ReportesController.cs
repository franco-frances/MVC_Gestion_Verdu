using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_GestionVerdu.Interfaces;
using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Controllers
{
    public class ReportesController : Controller
    {

        private readonly IReporteService _reporteService;

        public ReportesController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        public async Task<IActionResult> Index(DateTime? fechaInicio, DateTime? fechaFin, string intervalo)

        {
            intervalo = string.IsNullOrEmpty(intervalo) ? "diario" : intervalo;

            var modelo = await _reporteService.ObtenerReporteAsync(fechaInicio, fechaFin,intervalo);
            
            return View(modelo);
        }





    }
}
