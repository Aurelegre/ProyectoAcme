using System;
using System.Collections.Generic;

namespace Acme.Tablas;

public partial class Respuestum
{
    public int IdRespuesta { get; set; }

    public string? Contenido { get; set; }

    public int? Idcampo { get; set; }

    public virtual Campo? IdcampoNavigation { get; set; }
}
