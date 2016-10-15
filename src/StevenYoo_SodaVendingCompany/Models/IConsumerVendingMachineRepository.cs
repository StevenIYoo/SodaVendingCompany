using System.Collections.Generic;
using System.Threading.Tasks;

namespace StevenYoo_SodaVendingCompany.Models
{
    public interface IConsumerVendingMachineRepository
    {
        List<Soda> GetVendingSodas();
        int GetRemainingSodaCount(string sodaName);
        void VendSoda(string sodaName);
    }
}
