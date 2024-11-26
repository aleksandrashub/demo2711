using System;
using System.Collections.Generic;

namespace demo21111.Models;

public partial class Client
{
    public long IdClient { get; set; }

    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? Lastname { get; set; }

    public string? Login { get; set; }

    public string? Passwd { get; set; }

    public long? IdRole { get; set; }

    public virtual Role? IdRoleNavigation { get; set; }

    public virtual ICollection<Zakaz> Zakazs { get; set; } = new List<Zakaz>();
}
