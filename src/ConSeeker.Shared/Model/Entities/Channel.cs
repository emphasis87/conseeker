using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConSeeker.Shared.Model.Sources
{
    public class Channel
    {
        public string Link { get; set; }    
        public DateTime LastRead { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Content { get; set; }
    }
}
