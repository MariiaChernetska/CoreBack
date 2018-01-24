using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PillarInterview.Data.Repositories;
using PillarInterview.Services.Dictionaries;

namespace PillarInterview.Controllers
{
    [Produces("application/json")]
    [Route("api/dictionaries")]
    public class DictionariesController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public DictionariesController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpGet("customerTypes")]
        public IActionResult GetCustomerTypes()
        {
            var handler = new GetCustomerTypesHandler(this._unitOfWork);
            var result = handler.Execute();
            return new OkObjectResult(result);
        }
    }
}