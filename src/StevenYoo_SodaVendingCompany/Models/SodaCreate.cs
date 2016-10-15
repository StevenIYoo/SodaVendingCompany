using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StevenYoo_SodaVendingCompany.Models
{
    public class SodaCreate
    {
        public string SodaName { get; set; }
        public decimal SodaPrice { get; set; }
        public int SodaCount { get; set; }
        public string ImageUrl { get; set; }
    }
}
