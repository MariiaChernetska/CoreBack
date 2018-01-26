using Microsoft.AspNetCore.Mvc;
using PillarInterview.Data.Repositories;
using PillarInterview.Services.Dictionaries;
using Microsoft.AspNetCore.Authorization;
using PillarInterview.Data.Models;

namespace PillarInterview.Controllers
{
    [Authorize(Roles = Roles.AdminRole)]
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