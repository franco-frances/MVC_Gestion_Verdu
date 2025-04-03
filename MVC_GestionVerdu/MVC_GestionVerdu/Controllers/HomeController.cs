using Microsoft.AspNetCore.Mvc;
using MVC_GestionVerdu.Services.Interfaces;
using MVC_GestionVerdu.Models;
using System.Diagnostics;

namespace MVC_GestionVerdu.Controllers
{
    public class HomeController : Controller
    {
       

        public HomeController()
        {
            
        }

        public async Task< IActionResult> Index()
        {
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string mensaje = "Ocurrió un error inesperado.")
        {
            var model = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Mensaje = mensaje
            };

            return View(model);
        }
    }
}
