using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StevenYoo_SodaVendingCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StevenYoo_SodaVendingCompany.Controllers.Api
{
    [Authorize]
    [Route("/api/adminVendingMachine/sodas")]
    public class AdminVendingMachineController : Controller
    {
        private IAdminVendingMachineRepository _repository;

        public AdminVendingMachineController(IAdminVendingMachineRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("{sodaName}/{price}/priceChange")]
        public IActionResult Post(string sodaName, decimal price)
        {
            try
            {
                _repository.ChangeSodaCost(sodaName, price);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        [HttpPut("{sodaName}/{type}/count")]
        public IActionResult Put(string sodaName, string type)
        {
           if (type == "increment")
           {
                try
                {
                    return Ok(IncrementSodaCount(sodaName));
                }
                catch (Exception ex)
                {
                    throw new NotImplementedException();
                }

           }

           else if (type == "decrement")
            {
                try
                {
                    return Ok(DecrementSodaCount(sodaName));
                }
                catch (Exception ex)
                {
                    throw new NotImplementedException();
                }
            }
           else
            {
                return BadRequest("Type was not found");
            }
        }

        private int IncrementSodaCount(string sodaName)
        {
            try
            {
                var sodaCount = _repository.IncrementSodaCount(sodaName);

                return sodaCount;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }

        }

        private int DecrementSodaCount(string sodaName)
        {
            try
            {
                var sodaCount = _repository.DecrementSodaCount(sodaName);

                return sodaCount;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
    }
}
