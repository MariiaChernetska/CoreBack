using PillarInterview.Data.Repositories;
using PillarInterview.Services.Models;
using System.Linq;

namespace PillarInterview.Services.Customers
{
    public class GetCustomerHandler
    {
        IUnitOfWork _unitOfWork;

        public GetCustomerHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Gets customer full model
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <returns>CustomerSaveModel</returns>
        public CustomerSaveModel Execute(int customerId) {
            var customerFromDB = _unitOfWork.CustomerRepository.Get().Where(c => c.Id == customerId)
                                        .Select(c => new CustomerSaveModel
                                        {
                                            Id = c.Id,
                                            Name = c.Name,
                                            Address = c.Address,
                                            Comments = c.Comments,
                                            Email = c.Email,
                                            Phone = c.Phone,
                                            NumberOfSchools = c.NumberOfSchools,
                                            Type = c.TypeId,
                                            Contacts = c.Contacts.Select(q => new ContactSaveModel
                                            {
                                                Id = q.Id,
                                                Phone = q.Phone,
                                                Email = q.Email,
                                                Name = q.Name,
                                                Role = q.Role
                                            }).ToList(),
                                            Departments = c.Departments.Select(d => new DepartmentSaveModel
                                            {
                                                 Address = d.Address,
                                                  Id = d.Id,
                                                  ManagerLogin = d.DepartmentManager.User.UserName,
                                                  Name = d.Name
                                            }).ToList(),
                                            Users = c.Users.Select(u=>new UserSaveModel {
                                                Id = u.UserId,
                                                 Name = u.User.Name,
                                                  DepartmentName = u.Department.Name,
                                                   Email = u.User.Email,
                                                    Phone = u.User.PhoneNumber,
                                                     UserName = u.User.UserName
                                            }).ToList()
                                        }).SingleOrDefault();
        
            return customerFromDB;
        }
    }
}
