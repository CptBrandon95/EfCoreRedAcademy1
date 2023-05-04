using System;
using System.Collections.Generic;

namespace EfCoreRedAcademy1;

public partial class Class
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public long ProfessorsId { get; set; }

    public virtual Professor Professors { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
