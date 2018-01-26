using PillarInterview.Data.Repositories;
using PillarInterview.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PillarInterview.Services.Customers
{
    public class GetCustomerGeneralInfoHandler
    {
        IUnitOfWork _unitOfWork;

        public GetCustomerGeneralInfoHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Get customer general info
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>CustomerViewModel</returns>
        public CustomerViewModel Execute(int customerId)
        {
            var customerFromDB = _unitOfWork.CustomerRepository.Get().Where(c => c.Id == customerId)
                                       .Select(c => new CustomerViewModel
                                       {
                                           Id = c.Id,
                                           Name = c.Name,
                                           Address = c.Address,
                                           Comments = c.Comments,
                                           Email = c.Email,
                                           Phone = c.Phone,
                                           NumberOfSchools = c.NumberOfSchools,
                                           Type = c.Type.Title,
                                       }).SingleOrDefault();
            return customerFromDB;
        }
    }
}
