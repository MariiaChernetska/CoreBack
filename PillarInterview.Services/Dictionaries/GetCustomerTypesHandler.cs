using PillarInterview.Data.Repositories;
using PillarInterview.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PillarInterview.Services.Dictionaries
{
    public class GetCustomerTypesHandler
    {
        IUnitOfWork _unitOfWork;

        public GetCustomerTypesHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public List<CustomerTypeViewModel> Execute()
        {
            var result = this._unitOfWork.CustomerTypesRepository.Get().Select(e => new CustomerTypeViewModel
            {
                Id = e.Id,
                Title = e.Title
            }).ToList();

            return result;
        }
    }
}
