using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StevenYoo_SodaVendingCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StevenYoo_SodaVendingCompany.Controllers.Api
{
    [Authorize]
    [Route("/api/reportVendingMachine/sodas")]
    public class ReportVendingMachineController : Controller
    {
        private IReportVendingMachineRepository _repository;

        public ReportVendingMachineController(IReportVendingMachineRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var sodas = _repository.GetReport();

                return Ok(sodas);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }

            return BadRequest("Failed to get transaction report");
        }
    }
}
