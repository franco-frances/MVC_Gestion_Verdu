using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Repositories.Interfaces
{
    public interface IGastosRepository
    {
        Task AddAsync(Gastos gasto);
        Task DeleteAsync(int id);
        Task<IEnumerable<Gastos>> GetAllAsync(int idUsuario);
        Task<IEnumerable<Gastos>> GetByDayAysnc(int usuarioId, DateTime fechaActual);
        Task<Gastos> GetByIdAsync(int id);
        Task<(IEnumerable<Gastos> gastos, int totalRegistros, decimal totalMonto)> GetGastosPaginadosAsync(int usuarioId, DateTime? fechaInicio, DateTime? fechaFin, int pageNumber, int pageSize);
        Task UpdateAsync(Gastos gasto);
    }
}