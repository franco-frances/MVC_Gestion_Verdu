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
        public async Task<IActionResult> AgregarGasto(Gastos gasto) {


            gasto.UsuarioId = int.Parse(HttpContext.Session.GetString("UsuarioId"));




            await _gastoService.AgregarGasto(gasto);

            TempData["MensajeGasto"] = "Gasto agregado correctamente.";

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


            gastoEditado.Concepto = gasto.Concepto;
            gastoEditado.Fecha = gasto.Fecha;
            gastoEditado.Monto = gasto.Monto;



           


            await _gastoService.EditarGasto(gastoEditado);



            TempData["MensajeGastoEditado"] = "Gasto editado correctamente.";


            return View(gasto);







        }



    }
}
