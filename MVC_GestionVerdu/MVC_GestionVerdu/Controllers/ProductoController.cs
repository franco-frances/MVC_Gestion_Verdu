using Microsoft.AspNetCore.Mvc;
using MVC_GestionVerdu.Interfaces;
using MVC_GestionVerdu.Models;
using MVC_GestionVerdu.Services;

namespace MVC_GestionVerdu.Controllers
{
    public class ProductoController : Controller
    {

        private readonly ICategoriaService _categoriaService;
        private readonly IProductoService _productoService;


        public ProductoController(ICategoriaService categoriaService, IProductoService productoService)
        {

            _categoriaService = categoriaService;
            _productoService = productoService;
        }


        public async Task<IActionResult> Index()
        {

            var categorias = await _categoriaService.GetCategorias();

            var producto = new Producto{ };

            ViewBag.Categorias = categorias;



            return View(producto);
        }



        [HttpPost]


        public async Task<IActionResult> Create(Producto producto)
        {

            producto.UsuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));




            await _productoService.AgregarProducto(producto);

            TempData["MensajeAgregado"] = "Producto agregado correctamente.";



            return RedirectToAction("Index");



        }





        



    }
}
