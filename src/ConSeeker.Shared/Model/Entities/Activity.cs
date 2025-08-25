using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConSeeker.Shared.Model.Entities
{
    public class Activity
    {
        public long Id { get; set; }

        public string Title { get; set; } = default!;
        public string? Description { get; set; }

        public DateTime Begin { get; set; }
        public DateTime End { get; set; }

        public string? Line { get; set; }
        public string? Location { get; set; }

        public string? UIOrder { get; set; }

        public long HostId { get; set; }
        public Host Host { get; set; } = default!;
    }
}
