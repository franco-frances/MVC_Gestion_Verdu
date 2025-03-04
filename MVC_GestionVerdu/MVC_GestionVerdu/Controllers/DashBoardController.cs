using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_GestionVerdu.Interfaces;
using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Controllers
{
    public class DashBoardController : Controller
    {

        private readonly IProductoService _productoService;
        private readonly ICategoriaService _categoriaService;

        public DashBoardController(IProductoService productoService, ICategoriaService categoriaService)
        {
            
            _productoService = productoService;
            _categoriaService= categoriaService;
        }



        public async Task<IActionResult> Index()
        {

            int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));

            var productos= await _productoService.ListarProductos(usuarioId);

            TempData["Mensaje"] = "Producto agregado correctamente.";


            return View(productos);
        }



        public async Task<IActionResult> Editar(int id)
        {


            var producto = await _productoService.GetProducto(id);
            var categoria = await _categoriaService.GetCategorias();

            ViewBag.Categorias = categoria;

            
            return View(producto);




        }

        [HttpPost]


        public async Task<IActionResult> Editar(Producto producto) {


            var produEditado = await _productoService.GetProducto(producto.IdProductos);

            produEditado.Descripcion= producto.Descripcion;
            produEditado.CategoriaId= producto.CategoriaId;
            produEditado.PrecioCajon= producto.PrecioCajon;
            produEditado.PesoCajon= producto.PesoCajon;
            produEditado.MargenGanancia= producto.MargenGanancia;
            produEditado.PrecioCosto= producto.PrecioCosto;
            produEditado.PrecioFinal= producto.PrecioFinal;
            


            //pasa las categorias para que sean cargadas en select
            ViewBag.Categorias = await _categoriaService.GetCategorias();



            await _productoService.EditarProducto(produEditado);



            TempData["MensajeEditar"] = "Producto editado correctamente.";

            return View(producto);


        }


        [HttpPost]

        public async Task<IActionResult> Eliminar(int id)
        {

            await _productoService.EliminarProducto(id);
            
            
           
            return RedirectToAction("Index");




        }




    }
}
