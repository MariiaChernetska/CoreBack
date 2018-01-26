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

        public void Execute(int id)
        {
            var removeDepartamentHandler = new RemoveDepartmentHandler(_unitOfWork);
            var customerDeps = _unitOfWork.DepartmentRepository.Get().Where(e => e.CustomerId == id).ToList();
            foreach (var dep in customerDeps)
            {
                removeDepartamentHandler.Execute(dep.Id);
            }
            var contactsToRemove = _unitOfWork.ContactRepository.Get().Where(c => c.CustomerId == id).ToList();
            foreach (var item in contactsToRemove)
            {
                _unitOfWork.ContactRepository.Delete(item);
            }
            _unitOfWork.CustomerRepository.Delete(id);
            _unitOfWork.Save();
        }
    }
}
