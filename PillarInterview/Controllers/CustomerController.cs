using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PillarInterview.Data.Models;
using PillarInterview.Services.Models;
using PillarInterview.Data.Repositories;
using PillarInterview.Services.Customers;

namespace PillarInterview.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        [HttpPost]
        public IActionResult SaveCustomer([FromBody]CustomerSaveModel customerSaveModel)
        {
            if (customerSaveModel != null)
            {
                var handler = new CreateUpdateCustomerHandler(_unitOfWork);
                handler.Execute(customerSaveModel);
            }

            return new OkResult();
        }
    }
}