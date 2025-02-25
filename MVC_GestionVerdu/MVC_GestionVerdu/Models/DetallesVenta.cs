using System;
using System.Collections.Generic;

namespace MVC_GestionVerdu.Models;

public partial class DetallesVenta
{
    public int IdDetalleVenta { get; set; }

    public DateTime Fecha { get; set; }

    public int MetodoPagoId { get; set; }

    public string? Concepto { get; set; }

    public decimal Monto { get; set; }

    public int? UsuarioId { get; set; }

    public virtual MetodoPago? MetodoPago { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
