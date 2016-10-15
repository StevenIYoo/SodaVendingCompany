using Microsoft.AspNetCore.Mvc;
using StevenYoo_SodaVendingCompany.Data;
using StevenYoo_SodaVendingCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StevenYoo_SodaVendingCompany.Controllers.Api
{
    [Route("/api/CreateSoda/")]
    public class CreateSodaController
    {
        IDatabaseRepository _repository;

        public CreateSodaController (IDatabaseRepository repository)
        {
            _repository = repository;
        }


        [HttpPost("")]
        public void Post([FromBody] SodaCreate sodaCreate)
        {
            var newSoda = new CreateSodaData(_repository);

            newSoda.InsertNewSoda(sodaCreate.SodaName, sodaCreate.SodaCount, sodaCreate.SodaPrice, sodaCreate.ImageUrl);
        }
    }
}
