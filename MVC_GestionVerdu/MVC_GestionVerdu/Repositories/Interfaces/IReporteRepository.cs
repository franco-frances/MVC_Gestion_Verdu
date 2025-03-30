using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Repositories.Interfaces
{
    public interface IReporteRepository
    {
        Task<IEnumerable<Gastos>> ObtenerGastosAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<IEnumerable<DetallesVenta>> ObtenerIngresosAsync(DateTime fechaInicio, DateTime fechaFin);
    }
}