using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StevenYoo_SodaVendingCompany.Models
{
    public class Soda
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int SodaCount { get; set; }

        //public SodaCount SodaRemaining { get; set; }
    }
}
