using System;
using System.Collections.Generic;

namespace MVC_GestionVerdu.Models;

public partial class Gastos
{
    public int IdGasto { get; set; }

    public DateTime Fecha { get; set; }

    public string Concepto { get; set; } = null!;

    public decimal Monto { get; set; }

    public int? UsuarioId { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
