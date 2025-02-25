using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_GestionVerdu.Interfaces;

namespace MVC_GestionVerdu.Controllers
{
    public class DashBoardController : Controller
    {

        private readonly IProductoService _productoService;

        public DashBoardController(IProductoService productoService)
        {
            
            _productoService = productoService;
        }



        public async Task<IActionResult> Index()
        {

            var productos= await _productoService.ListarProductos();





            return View();
        }
    }
}
