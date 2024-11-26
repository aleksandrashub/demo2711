using System;
using System.Collections.Generic;

namespace demo21111.Models;

public partial class Zakaz
{
    public long IdZakaz { get; set; }

    public DateOnly? Datecr { get; set; }

    public DateOnly? Datedel { get; set; }

    public long? IdPunkt { get; set; }

    public long? IdUser { get; set; }

    public long? Code { get; set; }

    public string? Status { get; set; }

    public virtual Punkt? IdPunktNavigation { get; set; }

    public virtual Client? IdUserNavigation { get; set; }

    public virtual ICollection<ZakazPr> ZakazPrs { get; set; } = new List<ZakazPr>();
}
