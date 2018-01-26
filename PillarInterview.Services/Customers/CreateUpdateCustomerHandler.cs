using PillarInterview.Data.Repositories;
using PillarInterview.Services.Models;
using PillarInterview.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace PillarInterview.Services.Customers
{
    public class CreateUpdateCustomerHandler
    {
        IUnitOfWork _unitOfWork;

        public CreateUpdateCustomerHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Creates or updates customer
        /// </summary>
        /// <param name="customerSaveModel">Customer data of CustomerSaveModel type</param>
        /// <returns></returns>
        public bool Execute(CustomerSaveModel customerSaveModel)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                Customer customer = CreateUpdateCustomer(customerSaveModel);

                if (customer == null)
                {
                    transaction.Rollback();
                    return false;
                }

                var existingDepartments = customerSaveModel.Departments.Where(d => d.Id != 0)
                    .Select(e => e.Id).ToList();
                RemoveCustomerDepartments(existingDepartments, customer.Id);

                List<Department> departments = new List<Department>();

                foreach (var departmentSaveModel in customerSaveModel.Departments)
                {
                    Department department = CreateUpdateDepartment(departmentSaveModel, customer);
                    if (department == null)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    departments.Add(department);
                }

                var existingUsers = customerSaveModel.Users.Where(d => !string.IsNullOrWhiteSpace(d.Id))
                    .Select(e => e.Id).ToList();
                RemoveCustomerUsers(existingUsers, customer.Id);

                List<User> users = new List<User>();

                foreach (var userSaveModel in customerSaveModel.Users)
                {
                    User user = CreateUpdateUser(userSaveModel, customer, departments);
                    if (user == null)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    users.Add(user);
                }

                foreach (var department in departments)
                {
                    var departmentSM = customerSaveModel.Departments.FirstOrDefault(d => d.Name == department.Name);
                    var user = users.FirstOrDefault(u => u.UserName == departmentSM.ManagerLogin);
                    if (department.DepartmentManager == null)
                    {
                        department.DepartmentManager = new DepartmentManager { Department = department, User = user };
                    }
                    else
                    {
                        department.DepartmentManager.User = user;

                        _unitOfWork.DepartmentManagerRepository.Update(department.DepartmentManager);
                    }
                }

                var existingContacts = customerSaveModel.Contacts.Where(d => d.Id != 0)
                    .Select(e => e.Id).ToList();
                RemoveCustomerContacts(existingContacts, customer.Id);

                List<Contact> contacts = new List<Contact>();

                foreach (var contactSaveModel in customerSaveModel.Contacts)
                {
                    Contact contact = CreateUpdateContact(contactSaveModel, customer);
                    if (contact == null)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    contacts.Add(contact);
                }

                _unitOfWork.Save();
                transaction.Commit();
            }
            return true;

        }
        /// <summary>
        /// Removes users of customer
        /// </summary>
        /// <param name="existingUsers">customer users</param>
        /// <param name="customerId">customer id</param>
        private void RemoveCustomerUsers(List<string> existingUsers, int customerId)
        {
            
            var handler = new RemoveUserHandler(_unitOfWork);
            if (customerId == 0)
            {
                return;
            }
            else
            {
                var usersForDelete = _unitOfWork.UserManager.Users
                                        .Where(d => d.UserInfo.CustomerId == customerId && !existingUsers.Contains(d.Id)).ToList();
                foreach (var d in usersForDelete)
                {
                    handler.Execute(d.Id);
                }

            }
        }
        /// <summary>
        /// Removes customer departments
        /// </summary>
        /// <param name="existingDepartments">customer departments</param>
        /// <param name="customerId">customer id</param>
        private void RemoveCustomerDepartments(List<int> existingDepartments, int customerId)
        {
            var handler = new RemoveDepartmentHandler(_unitOfWork);
            if (customerId == 0)
            {
                return;
            }
            else
            {
                var departmentsForDelete = _unitOfWork.DepartmentRepository.Get()
                                        .Where(d => d.CustomerId == customerId && !existingDepartments.Contains(d.Id)).ToList();
                foreach (var d in departmentsForDelete)
                {
                    handler.Execute(d.Id);
                }

            }

        }

        private void RemoveCustomerContacts(List<int> existingContacts, int customerId)
        {
            if (customerId == 0)
            {
                return;
            }
            else
            {
                var contactsToDelete = _unitOfWork.ContactRepository.Get()
                                        .Where(d => d.CustomerId == customerId && !existingContacts.Contains(d.Id)).ToList();
                foreach (var с in contactsToDelete)
                {
                    _unitOfWork.ContactRepository.Delete(с);
                }

            }

        }

        private Department CreateUpdateDepartment(DepartmentSaveModel departmentSaveModel, Customer customer)
        {
            Department department;
            if (departmentSaveModel.Id == 0)
            {
                department = new Department();
            }
            else
            {
                department = _unitOfWork.DepartmentRepository.Get(e => e.DepartmentManager).Where(e => e.Id == departmentSaveModel.Id).SingleOrDefault();
                if (customer == null)
                {
                    return null;
                }
            }

            department.Name = departmentSaveModel.Name;
            department.Address = departmentSaveModel.Address;
            department.Customer = customer;
            _unitOfWork.DepartmentRepository.Update(department);
            return department;

        }

        private Contact CreateUpdateContact(ContactSaveModel contactSaveModel, Customer customer)
        {
            Contact contact;
            if (contactSaveModel.Id == 0)
            {
                contact = new Contact();
            }
            else
            {
                contact = _unitOfWork.ContactRepository.Get(contactSaveModel.Id);
                if (customer == null)
                {
                    return null;
                }
            }

            contact.Name = contactSaveModel.Name;
            contact.Phone = contactSaveModel.Phone;
            contact.Role = contactSaveModel.Role;
            contact.Email = contactSaveModel.Email;
            contact.Customer = customer;
            _unitOfWork.ContactRepository.Update(contact);
            return contact;
        }

        private User CreateUpdateUser(UserSaveModel userSaveModel, Customer customer, List<Department> departments)
        {
            User user;
            if (string.IsNullOrWhiteSpace(userSaveModel.Id))
            {
                user = new User();
                user.Name = userSaveModel.Name;
                user.Email = userSaveModel.Email;

                user.PhoneNumber = userSaveModel.Phone;
                user.UserName = userSaveModel.UserName;
                user.UserInfo = new UserInfo
                {
                    User = user,
                    Customer = customer,
                    Department = departments.FirstOrDefault(d => d.Name == userSaveModel.DepartmentName),
                };
                var res = _unitOfWork.UserManager.CreateAsync(user, userSaveModel.Password).GetAwaiter().GetResult();
                if (res.Succeeded)
                {
                   var addToRoleRes = _unitOfWork.UserManager.AddToRoleAsync(user, Roles.UserRole).GetAwaiter().GetResult();
                    if (!res.Succeeded) {
                        return null;
                    }
                }
                else {
                    return null;
                }
            }
            else
            {
                user = _unitOfWork.UserRepository.Get(e=>e.UserInfo).FirstOrDefault(u => u.Id == userSaveModel.Id);
                if (user == null)
                {
                    return null;
                }
                else
                {
                    user.Name = userSaveModel.Name;
                    user.PhoneNumber = userSaveModel.Phone;
                    user.UserName = userSaveModel.UserName;
                    user.Email = userSaveModel.Email;
                    if(user.UserInfo == null)
                    {
                        user.UserInfo = new UserInfo { User = user };
                        user.UserInfo.Department = departments.FirstOrDefault(d => d.Name == userSaveModel.DepartmentName);
                    }

                    var res =_unitOfWork.UserManager.UpdateAsync(user).GetAwaiter().GetResult();
                    if (!res.Succeeded) {
                        return null;
                    }
                }
            }




            return user;

        }

        private Customer CreateUpdateCustomer(CustomerSaveModel customerSaveModel)
        {
            Customer customer;
            if (customerSaveModel.Id == 0)
            {
                customer = new Customer();

            }
            else
            {
                customer = _unitOfWork.CustomerRepository.Get(customerSaveModel.Id);
                if (customer == null)
                {
                    return null;
                }
            }
            customer.Name = customerSaveModel.Name;
            customer.Phone = customerSaveModel.Phone;
            customer.Address = customerSaveModel.Address;
            customer.NumberOfSchools = customerSaveModel.NumberOfSchools;
            customer.TypeId = customerSaveModel.Type;
            customer.Comments = customerSaveModel.Comments;
            customer.Email = customerSaveModel.Email;
            _unitOfWork.CustomerRepository.Update(customer);
            return customer;
        }

    }
}
