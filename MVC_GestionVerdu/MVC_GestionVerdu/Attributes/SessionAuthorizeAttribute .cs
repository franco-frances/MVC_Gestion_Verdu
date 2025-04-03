using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MVC_GestionVerdu.Attributes
{
    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Obtener el valor de "UsuarioId" desde la sesión
            var usuarioId = context.HttpContext.Session.GetString("UsuarioId");

            // Si el usuario no está autenticado, redirigir al Login
            if (string.IsNullOrEmpty(usuarioId))
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }

            base.OnActionExecuting(context);
        }



    }
}
