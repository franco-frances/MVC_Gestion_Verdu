using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Services.Interfaces
{
    public interface IReporteService
    {

        Task<ReporteViewModel> ObtenerReporteAsync(DateTime? fechaInicio, DateTime? fechaFin, string intervalo);


    }
}
