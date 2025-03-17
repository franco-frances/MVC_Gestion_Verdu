using Microsoft.AspNetCore.Mvc;
using MVC_GestionVerdu.Interfaces;
using MVC_GestionVerdu.Models;

namespace MVC_GestionVerdu.Controllers
{
    public class GastosController : Controller
    {

        private readonly IGastoService _gastoService;

        public GastosController(IGastoService gastoService)
        {
            
            _gastoService = gastoService;
            
        }



        public async Task<IActionResult> Index()
        {       
            int usuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));
            
            var gastos = await _gastoService.GetGastos(usuarioId);

                                

            return View(gastos);
        }


        public IActionResult AgregarGasto()
        {
            var model = new Gastos
            {
                Fecha = DateTime.Today // Asigna la fecha de hoy
            };
            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> AgregarGasto(Gastos gasto)
        {
            gasto.UsuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));
            await _gastoService.AgregarGasto(gasto);

            return Json(new { success = true, message = "Gasto agregado correctamente.", gasto });
        }



        public async Task<IActionResult> Editar(int id) {


            var gasto = await _gastoService.GetGastoById(id);

            return View(gasto);
        
        
        }


        [HttpPost]
        public async Task<IActionResult> Editar(Gastos gasto)
        {
            var gastoEditado = await _gastoService.GetGastoById(gasto.IdGasto);

            if (gastoEditado == null)
            {
                return Json(new { success = false, message = "El gasto no existe." });
            }

            try
            {
                gastoEditado.Concepto = gasto.Concepto;
                gastoEditado.Fecha = gasto.Fecha;
                gastoEditado.Monto = gasto.Monto;

                await _gastoService.EditarGasto(gastoEditado);

                return Json(new { success = true, message = "Gasto editado correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al editar el gasto: " + ex.Message });
            }
        }


        [HttpPost]


        public async Task<IActionResult> Eliminar(int id)
        {

            await _gastoService.EliminarGasto(id);

            return RedirectToAction("Index");


        }




    }
}
