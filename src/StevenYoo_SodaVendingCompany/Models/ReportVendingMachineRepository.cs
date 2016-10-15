using StevenYoo_SodaVendingCompany.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StevenYoo_SodaVendingCompany.Models
{
    public class ReportVendingMachineRepository : IReportVendingMachineRepository
    {
        IReportVendingMachineData _repository;
        
        public ReportVendingMachineRepository(IReportVendingMachineData repository)
        {
            _repository = repository;
        }

        public List<SodaReportTransaction> GetReport()
        {
            return _repository.GetReport();
        }
    }
}
