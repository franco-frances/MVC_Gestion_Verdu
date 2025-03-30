using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        Task AddAsync(Categoria categoria);
        Task DeleteAsync(int id);
        Task<IEnumerable<Categoria>> GetAllAsync();
        Task<Categoria> GetbyIdAsync(int id);
        Task UpdateAsync(Categoria categoria);
    }
}