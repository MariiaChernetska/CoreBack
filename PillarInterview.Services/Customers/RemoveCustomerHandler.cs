using PillarInterview.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PillarInterview.Services.Customers
{
    public class RemoveCustomerHandler
    {
        IUnitOfWork _unitOfWork;

        public RemoveCustomerHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Removes customer
        /// </summary>
        /// <param name="id">customer id</param>
        public void Execute(int id)
        {
            
            var removeDepartamentHandler = new RemoveDepartmentHandler(_unitOfWork);
            var customerDeps = _unitOfWork.DepartmentRepository.Get().Where(e => e.CustomerId == id).ToList();
            //delete departments of the customer with inner users deleting
            foreach (var dep in customerDeps)
            {
                removeDepartamentHandler.Execute(dep.Id);
            }
            //remove contacts of the customer
            var contactsToRemove = _unitOfWork.ContactRepository.Get().Where(c => c.CustomerId == id).ToList();
            foreach (var item in contactsToRemove)
            {
                _unitOfWork.ContactRepository.Delete(item);
            }
            //delete customer
            _unitOfWork.CustomerRepository.Delete(id);
            _unitOfWork.Save();
        }
    }
}
