namespace MVC_GestionVerdu.Models
{
    public class ReporteViewModel
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<string> Fechas { get; set; } = new();
        public List<decimal> Ingresos { get; set; } = new();
        public List<decimal> Gastos { get; set; } = new();
        public List<decimal> Balance { get; set; } = new(); //  Nueva propiedad para balance
        public string Intervalo { get; set; }
    }
}
