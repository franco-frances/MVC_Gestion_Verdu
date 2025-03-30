using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Repositories.Interfaces
{
    public interface IMetodoPagosRepository
    {
        Task AddAsync(MetodoPago metodoPago);
        Task DeleteAsync(int id);
        Task<IEnumerable<MetodoPago>> GetAllasync();
        Task<MetodoPago> GetByIdAsync(int id);
        Task UpdateAsync(MetodoPago metodoPago);
    }
}