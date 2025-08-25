using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConSeeker.Shared.Model.Entities
{
    public class Convention
    {
        public long Id { get; set; }

        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public DateTime Begin { get; set; }
        public DateTime End { get; set; }

        public long ProviderId { get; set; }
        public Provider Provider { get; set; } = default!;
    }
}
