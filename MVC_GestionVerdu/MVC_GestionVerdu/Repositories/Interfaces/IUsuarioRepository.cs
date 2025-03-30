using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task AddAsync(Usuario usuario);
        Task DeleteAsync(int id);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task UpdateAsync(Usuario usuario);
    }
}