using StevenYoo_SodaVendingCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StevenYoo_SodaVendingCompany.Controllers.Api
{
    [Route("/api/consumerVendingMachine/sodas")]
    public class ConsumerVendingMachineController : Controller
    {
        private IConsumerVendingMachineRepository _repository;

        public ConsumerVendingMachineController(IConsumerVendingMachineRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var sodas = _repository.GetVendingSodas();

                return Ok(sodas);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }

            return BadRequest("Failed to get sodas on sale");
        }

        [HttpGet("{sodaName}/count")]
        public IActionResult Get(string sodaName)
        {
            try
            {
                return Ok(_repository.GetRemainingSodaCount(sodaName));
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }

            return BadRequest("Failed to get soda count");
        }

        [HttpPost("{sodaName}/count")]
        public IActionResult Post(string sodaName)
        {
            try
            {
                _repository.VendSoda(sodaName);

                return Created($"/api/consumerVendingMachine/sodas/{sodaName}/count", "");
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }

            return BadRequest("Failed to get soda count");
        }
    }
}
