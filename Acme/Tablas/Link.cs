using System;
using System.Collections.Generic;

namespace Acme.Tablas;

public partial class Link
{
    public int Idlink { get; set; }

    public string? Link1 { get; set; }

    public int Idencuesta { get; set; }

    public virtual Encuestum? IdencuestaNavigation { get; set; }
}
