using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StevenYoo_SodaVendingCompany.Models
{
    public interface IAdminVendingMachineRepository
    {

        int DecrementSodaCount(string sodaName);
        int IncrementSodaCount(string sodaName);
        int BulkUpdateSodaCount(string sodaName, int bulkUpdateAmount);

        void ChangeSodaCost(string sodaName, decimal newPrice);

    }
}
