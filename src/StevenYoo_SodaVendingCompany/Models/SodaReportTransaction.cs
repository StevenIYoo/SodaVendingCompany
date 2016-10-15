using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StevenYoo_SodaVendingCompany.Models
{
    public class SodaReportTransaction
    {
        public string SodaName { get; set; }
        public Decimal SodaPrice { get; set; }
        public DateTime TransactionDate { get; set; }

    }
}
