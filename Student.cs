using System;
using System.Collections.Generic;

namespace EfCoreRedAcademy1;

public partial class Student
{
    public long Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public long AddressesId { get; set; }

    public virtual Address Addresses { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
