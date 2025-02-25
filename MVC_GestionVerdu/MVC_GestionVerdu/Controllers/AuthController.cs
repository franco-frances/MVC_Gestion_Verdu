using Microsoft.AspNetCore.Mvc;
using MVC_GestionVerdu.Services;
using MVC_GestionVerdu.Models;
using MVC_GestionVerdu.Interfaces;


namespace MVC_GestionVerdu.Controllers
{
    public class AuthController : Controller
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService )
        {
             
            _authService = authService;
        }





        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model) // Cambia a Login
        {
            if (!ModelState.IsValid) // Si el modelo no es válido, devolver la vista con errores
            {
                return View(model);
            }

            try
            {
                var usuario = await _authService.Login(model); // Asegúrate de que tu método Login acepte Login

                if (usuario == null) // Si la autenticación falla
                {
                    ModelState.AddModelError("", "Email o contraseña incorrecta");
                    return View(model);
                }

                // Guardar datos en la sesión
                HttpContext.Session.SetString("UsuarioId", usuario.Id.ToString());
                HttpContext.Session.SetString("UsuarioNombre", usuario.NickName);

                return RedirectToAction("Index", "DashBoard");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }








        public IActionResult Register() { 
        
            return View();
        
        
        }


        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid) // Validar el modelo
            {
                try
                {
                    var usuario = await _authService.Register(model);
                    
                    return RedirectToAction("Login"); 
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ErrorRegister", ex.Message);
                }
            }
            return View(); 
        }










    }
}
