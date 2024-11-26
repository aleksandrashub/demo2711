using System;
using System.Collections.Generic;

namespace demo21111.Models;

public partial class Role
{
    public long IdRole { get; set; }

    public string? Role1 { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
