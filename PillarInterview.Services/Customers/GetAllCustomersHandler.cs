using PillarInterview.Data.Repositories;
using PillarInterview.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PillarInterview.Services.Customers
{
    public class GetAllCustomersHandler
    {
        IUnitOfWork _unitOfWork;

        public GetAllCustomersHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public List<CustomerViewModel> Execute()
        {
            var customerList = _unitOfWork.CustomerRepository.Get().Select(c => new CustomerViewModel
            {
                Id = c.Id,
                Address = c.Address,
                Comments = c.Comments,
                Email = c.Email,
                Name = c.Name,
                NumberOfSchools = c.NumberOfSchools,
                Phone = c.Phone,
                Type = c.Type.Title
            }).ToList();
            return customerList;
        }
        }
    }