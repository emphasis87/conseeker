using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConSeeker.Shared.Model.Entities
{
    public class Host
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
        public string? Aliases { get; set; }
        public string? Bio { get; set; }

        public ICollection<Activity> Activities { get; set; } = default!;
    }
}
