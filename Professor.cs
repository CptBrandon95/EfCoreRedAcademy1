using System;
using System.Collections.Generic;

namespace EfCoreRedAcademy1;

public partial class Professor
{
    public long Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public long AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
