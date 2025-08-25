using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConSeeker.Shared.Model.Entities
{
    public class Provider
    {
        public long Id { get; set; }

        public string SourceUrl { get; set; } = default!;
        public string? Name { get; set; }
        //public DateTime LastChecked { get; set; }
        //public DateTime LastChanged { get; set; }
        //public string? Content { get; set; }

        public List<Convention> Conventions { get; set; } = [];
    }
}
