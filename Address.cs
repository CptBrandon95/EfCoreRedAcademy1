using System;
using System.Collections.Generic;

namespace EfCoreRedAcademy1;

public partial class Address
{
    public long Id { get; set; }

    public string City { get; set; } = null!;

    public string Zip { get; set; } = null!;

    public string Street { get; set; } = null!;

    public long HouseNumber { get; set; }

    public virtual ICollection<Professor> Professors { get; set; } = new List<Professor>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
