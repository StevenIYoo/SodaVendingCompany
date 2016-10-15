using StevenYoo_SodaVendingCompany.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StevenYoo_SodaVendingCompany.Models
{
    public class ConsumerVendingMachineRepository : IConsumerVendingMachineRepository
    {
        private IConsumerVendingMachineData _repository;

        public ConsumerVendingMachineRepository(IConsumerVendingMachineData repository)
        {
            _repository = repository;
        }

        public int GetRemainingSodaCount(string sodaName)
        {
            return _repository.CheckRemainingSodaCount(sodaName);
        }

        public List<Soda> GetVendingSodas()
        {
            return _repository.SelectSodasForSale();
        }

        public void VendSoda(string sodaName)
        {
            _repository.DecrementSodaCountByOne(sodaName);
        }
    }
}
