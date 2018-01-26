using Microsoft.AspNetCore.Mvc;
using PillarInterview.Services.Models;
using PillarInterview.Data.Repositories;
using PillarInterview.Services.Customers;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using PillarInterview.Data.Models;

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
        [Authorize(Roles = Roles.AdminRole)]

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
        [HttpGet("{id}")]
        [Authorize(Roles = Roles.AdminRole)]

        public IActionResult Get(int id) {
            var handler = new GetCustomerHandler(_unitOfWork);
            var res = handler.Execute(id);
           return new OkObjectResult(res);
        }
        [Authorize(Roles = Roles.AdminRole)]

        [HttpGet]
        public IActionResult GetAll()
        {
            var handler = new GetAllCustomersHandler(_unitOfWork);
            var res = handler.Execute();
            return new OkObjectResult(res);
        }

        [HttpGet("forUser")]
        [Authorize]
        public IActionResult GetForUser()
        {
            var user = _unitOfWork.UserManager.GetUserAsync(User).GetAwaiter().GetResult();
            var userInfo = _unitOfWork.UserInfoRepository.Get().SingleOrDefault(e => e.UserId == user.Id);
            if(userInfo == null)
            {
                return Ok();
            }

            var handler = new GetCustomerGeneralInfoHandler(_unitOfWork);
            var res = handler.Execute(userInfo.CustomerId);
            return Ok(res);
        }
        [Authorize(Roles = Roles.AdminRole)]

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var handler = new RemoveCustomerHandler(_unitOfWork);
            handler.Execute(id);
            return new OkResult();
        }

    }
}