using StevenYoo_SodaVendingCompany.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StevenYoo_SodaVendingCompany.Models
{
    public class AdminVendingMachineRepository : IAdminVendingMachineRepository
    {
        private IAdminVendingMachineData _repository;

        public AdminVendingMachineRepository(IAdminVendingMachineData repository)
        {
            _repository = repository;
        }

        public int BulkUpdateSodaCount(string sodaName, int bulkUpdateAmount)
        {
            return _repository.BulkUpdateSodaCount(sodaName, bulkUpdateAmount);
        }

        public void ChangeSodaCost(string sodaName, decimal newPrice)
        {
            _repository.ChangeSodaCost(sodaName, newPrice);
        }

        public int DecrementSodaCount(string sodaName)
        {
            return _repository.DecrementSodaCount(sodaName);
        }

        public int IncrementSodaCount(string sodaName)
        {
            return _repository.IncrementSodaCount(sodaName);
        }
    }
}
