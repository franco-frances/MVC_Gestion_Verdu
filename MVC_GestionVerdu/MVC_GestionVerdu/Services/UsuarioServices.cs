using MVC_GestionVerdu.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MVC_GestionVerdu.Services.Interfaces;
using MVC_GestionVerdu.Repositories.Interfaces;


namespace MVC_GestionVerdu.Services
{


    public class UsuarioServices:IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioServices(IUsuarioRepository usuarioRepository)
        {

            _usuarioRepository = usuarioRepository;
        }




        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            return await _usuarioRepository.GetAllAsync();


        }



        public async Task<Usuario> GetUsuariosById(int id)
        {

            return await _usuarioRepository.GetByIdAsync(id);


        }


    
        public async Task AgregarUsuario(Usuario usuario)
        {

           await _usuarioRepository.AddAsync(usuario);

           
        }



        

        public async Task EditarUsuario(Usuario usuario)
        {

            await _usuarioRepository.UpdateAsync(usuario);

           

        }

      

        public async Task EliminarUsuario(int id)
        {

            await _usuarioRepository.DeleteAsync(id);

           
        }




    }
}
