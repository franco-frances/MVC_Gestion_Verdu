namespace MVC_GestionVerdu.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string Mensaje { get; set; } = "Ocurrió un error inesperado."; // Mensaje por defecto
    }
}
