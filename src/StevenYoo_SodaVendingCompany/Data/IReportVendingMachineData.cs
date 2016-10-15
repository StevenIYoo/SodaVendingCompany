using StevenYoo_SodaVendingCompany.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StevenYoo_SodaVendingCompany.Data
{
    public interface IReportVendingMachineData
    {
        List<SodaReportTransaction> GetReport();
    }
}
