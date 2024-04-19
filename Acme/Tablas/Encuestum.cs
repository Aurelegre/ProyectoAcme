using System;
using System.Collections.Generic;

namespace Acme.Tablas;

public partial class Encuestum
{
    public int IdEncuesta { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Campo> Campos { get; set; } = new List<Campo>();

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();
}
