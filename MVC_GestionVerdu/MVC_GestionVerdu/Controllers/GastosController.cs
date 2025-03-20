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
        public async Task<IActionResult> AgregarGasto(Gastos gasto, string origen)
        {

            if (origen=="gastosRapidos")
            {
                gasto.Concepto = "varios";
                
                
            }
            gasto.UsuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));


            await _gastoService.AgregarGasto(gasto);
            
            TempData["MensajeGastos"] = "Gasto agregado correctamente.";
            TempData["TipoMensajeGastos"] = "success"; // Para SweetAlert

            if (origen == "gastosRapidos")
            {
                
                return RedirectToAction("Index","DashBoard");


            }



            return RedirectToAction("Index");
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
                TempData["MensajeGastos"] = "El gasto no existe.";
                TempData["TipoMensajeGastos"] = "error";
                return RedirectToAction("Index");
            }

            try
            {
                gastoEditado.Concepto = gasto.Concepto;
                gastoEditado.Fecha = gasto.Fecha;
                gastoEditado.Monto = gasto.Monto;

                await _gastoService.EditarGasto(gastoEditado);

                TempData["MensajeGastos"] = "Gasto editado correctamente.";
                TempData["TipoMensajeGastos"] = "success";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["MensajeGastos"] = "Error al editar el gasto: " + ex.Message;
                TempData["TipoMensajeGastos"] = "error";
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var gasto = await _gastoService.GetGastoById(id);

            if (gasto == null)
            {
                return Json(new { success = false, message = "El gasto no existe." });
            }

            try
            {
                await _gastoService.EliminarGasto(id);
                return Json(new { success = true, message = "Gasto eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al eliminar el gasto: " + ex.Message });
            }
        }




    }
}
