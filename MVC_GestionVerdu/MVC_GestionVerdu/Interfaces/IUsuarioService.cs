using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Interfaces
{
    public interface IUsuarioService
    {

            Task<IEnumerable<Usuario>> GetUsuarios();
            Task<Usuario> GetUsuariosById(int id);
            Task AgregarUsuario(Usuario usuario);
            Task EditarUsuario(Usuario usuario);
            Task EliminarUsuario(int id);

                      

    }
}
