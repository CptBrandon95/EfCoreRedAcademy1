using EfCoreAcademy.Mode;

namespace EfCoreAcademy.Model
{
    public class Class : BaseEntity
    {
        public string Title { get; set; } = default!;
        public List<Student> Students { get; set; } = default!;
        public Professors Professors { get; set; } = default!;

    }
}
