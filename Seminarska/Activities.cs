using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminarska
{
    public class Activities
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan ActivityLength { get; set; }
        public int Distance { get; set; }
        public int Climb { get; set; }
    }
}
