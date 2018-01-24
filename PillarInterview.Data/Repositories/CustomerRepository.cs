using Microsoft.EntityFrameworkCore;
using PillarInterview.Data.IRepositories;
using PillarInterview.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarInterview.Data.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        ApplicationDbContext _dbContext;
        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<Customer> Get()
        {
            return _dbContext.Customers;
        }

        public Customer Get(int id)
        {
            return _dbContext.Customers.Find(id);
        }

        public Customer Create(Customer customer)
        {
           var saved =_dbContext.Customers.Add(customer);
           return saved.Entity;
        }

        public Customer Update(Customer customer)
        {
            var saved = _dbContext.Update(customer);
            return saved.Entity;
        }

        public void Delete(int id)
        {
            Customer customer = _dbContext.Customers.Find(id);
            if (customer != null)
                _dbContext.Customers.Remove(customer);
        }
    }
}
