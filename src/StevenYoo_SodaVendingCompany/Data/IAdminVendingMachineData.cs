using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StevenYoo_SodaVendingCompany.Data
{
    public interface IAdminVendingMachineData
    {
        int IncrementSodaCount(string sodaName);
        int DecrementSodaCount(string sodaName);
        int BulkUpdateSodaCount(string sodaName, int bulkUpdateAmount);

        void ChangeSodaCost(string sodaName, decimal newPrice);
    }
}
