using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StevenYoo_SodaVendingCompany.Models
{
    public interface IReportVendingMachineRepository
    {
        List<SodaReportTransaction> GetReport();
    }
}
