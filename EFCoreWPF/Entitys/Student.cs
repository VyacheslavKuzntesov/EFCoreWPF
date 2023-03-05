using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreWPF.Entitys
{
    public class Student
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public List<Visit> Visits { get; set; }
        public Group Group { get; set; }

        public override string ToString() => Name;
    }
}
