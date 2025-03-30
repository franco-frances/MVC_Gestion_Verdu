using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Repositories.Interfaces
{
    public interface IProductoRepository
    {
        Task AddAsync(Producto producto);
        Task DeleteAsync(int id);
        Task<IEnumerable<Producto>> GetAllAsync(int usuarioId);
        Task<Producto> GetbyIdAsync(int id);
        Task UpdateAsync(Producto producto);
    }
}