using Microsoft.AspNetCore.Identity;
using PillarInterview.Data.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace PillarInterview.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context, UserManager<User> userManager)
        {
            this._context = context;
            this.CustomerRepository = new GenericRepository<Customer>(_context);
            this.CustomerTypesRepository = new GenericRepository<CustomerType>(_context);
            this.DepartmentRepository = new GenericRepository<Department>(_context);
            this.ContactRepository = new GenericRepository<Contact>(_context);
            this.DepartmentManagerRepository = new GenericRepository<DepartmentManager>(_context);
            this.UserManager = userManager;
        }

        public GenericRepository<Customer> CustomerRepository { get; }

        public GenericRepository<CustomerType> CustomerTypesRepository { get; }

        public GenericRepository<Department> DepartmentRepository { get; }

        public GenericRepository<Contact> ContactRepository { get; }

        public UserManager<User> UserManager { get; }

        public GenericRepository<DepartmentManager> DepartmentManagerRepository { get; }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
