using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreWPF.Entitys
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Student>? Students { get; set; }

        public int? StudentCount => Students?.Count;

        public override string ToString() => $"{Name} +({StudentCount}) ";
    }
}
