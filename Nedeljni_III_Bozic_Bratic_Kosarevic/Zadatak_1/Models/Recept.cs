using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Models
{
    public class Recept
    {
        public int ReceptId { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public string ReceptName { get; set; }
        public string ReceptType { get; set; }
        public int PersonNumber { get; set; }
        public string Author { get; set; }
        public string ReceptText { get; set; }
        public DateTime CreationDate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public int ComponentsNumber { get; set; }

    }
}
