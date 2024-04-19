using System;
using System.Collections.Generic;

namespace Acme.Tablas;

public partial class Campo
{
    public int Idcampo { get; set; }

    public string? Nombre { get; set; }

    public string? Titulo { get; set; }

    public string Requerido { get; set; }

    public string? Tipo { get; set; }

    public int? IdEncuesta { get; set; }

    public virtual Encuestum? IdEncuestaNavigation { get; set; }

    public virtual ICollection<Respuestum> Respuesta { get; set; } = new List<Respuestum>();
}
