using MVC_GestionVerdu.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MVC_GestionVerdu.Services.Interfaces;
using MVC_GestionVerdu.Repositories.Interfaces;



namespace MVC_GestionVerdu.Services
{


    public class CategoriaServices:ICategoriaService
    {

        private readonly ICategoriaRepository _categoriaRepository;


        public CategoriaServices(ICategoriaRepository categoriaRepository)
        {

            _categoriaRepository = categoriaRepository;

        }




        public async Task<IEnumerable<Categoria>> GetCategorias()
        {

            return await _categoriaRepository.GetAllAsync();


        }

        

        public async Task<Categoria> GetCategoriabyId(int id)
        {

            
            return await _categoriaRepository.GetbyIdAsync(id);


        }



        public async Task AgregarCategoria( Categoria categoria)
        {
            await _categoriaRepository.AddAsync(categoria);

        }


        public async Task EditarCategoria(Categoria categoria)
        {

           await _categoriaRepository.UpdateAsync(categoria);
           

        }



       

        public async Task EliminarCategoria(int id)
        {
         
           await _categoriaRepository.DeleteAsync(id);

        }









    }
}
