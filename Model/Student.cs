﻿namespace EfCoreRedAcademy1.Model
{
    public class Student : BaseEntity
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;

        public List<Class> Classes { get; set; } = default!;
        public Address Addresses { get; set; } = default!;
    }
}
