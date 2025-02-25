using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Interfaces
{
    public interface ICategoriaService
    {

        Task<IEnumerable<Categoria>> GetCategorias();
        Task<Categoria> GetCategoriabyId(int id);
        Task AgregarCategoria(Categoria categoria);
        Task EditarCategoria(Categoria categoria);
        Task EliminarCategoria(int id);


    }
}
