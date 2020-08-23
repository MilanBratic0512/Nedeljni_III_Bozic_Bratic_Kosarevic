using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Models
{
    public class Components
    {
        public int ComponentId { get; set; }
        public int ReceptId { get; set; }
        public string ComponentName { get; set; }
        public int ComponentAmount { get; set; }
        public bool CanAddToCart { get; set; }
    }
}
