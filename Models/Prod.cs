using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;

namespace demo21111.Models;

public partial class Prod
{
    public long IdProd { get; set; }

    public string? Articul { get; set; }

    public string? Name { get; set; }

    public string? Edizm { get; set; }

    public long? Cost { get; set; }

    public string? Manuf { get; set; }

    public string? Suppl { get; set; }

    public string? Categ { get; set; }

    public float? Currdisc { get; set; }

    public float? Maxdisc { get; set; }

    public long? Quanskl { get; set; }

    public string? Descr { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<ZakazPr> ZakazPrs { get; set; } = new List<ZakazPr>();

    public Bitmap bitmap => Image != null && Image != "" ? new Bitmap($@"Assets\\{Image}") : null;
    public float? ItogCost => (Cost - (Cost * Currdisc / 100));
    public string color => Currdisc > 15 ? "#7fff00" : "White";

}
