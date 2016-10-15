using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StevenYoo_SodaVendingCompany.Models
{
    public class SodaTransaction
    {
        public int Id { get; set; }
        public int SodaName { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal SodaPrice { get; set; }


    }
}
