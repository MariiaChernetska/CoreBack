using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using PillarInterview.Data.Models;

namespace PillarInterview.Data.Repositories
{
    public interface IUnitOfWork
    {
        GenericRepository<Contact> ContactRepository { get; }
        GenericRepository<Customer> CustomerRepository { get; }
        GenericRepository<CustomerType> CustomerTypesRepository { get; }
        GenericRepository<Department> DepartmentRepository { get; }
        GenericRepository<DepartmentManager> DepartmentManagerRepository { get; }
        UserManager<User> UserManager { get; }

        IDbContextTransaction BeginTransaction();

        int Save();
    }
}