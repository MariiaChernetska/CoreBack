using PillarInterview.Data.IRepositories;
using PillarInterview.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PillarInterview.Data.Repositories
{
    public class CustomerTypesRepository : IRepository<CustomerType>
    {
        ApplicationDbContext _dbContext;
        public CustomerTypesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<CustomerType> Get()
        {
            return _dbContext.CustomerTypes;
        }

        public CustomerType Get(int id)
        {
            return _dbContext.CustomerTypes.Find(id);
        }

        public CustomerType Create(CustomerType customerType)
        {
            var saved = _dbContext.CustomerTypes.Add(customerType);
            return saved.Entity;
        }

        public CustomerType Update(CustomerType customerType)
        {
            var saved = _dbContext.Update(customerType);
            return saved.Entity;
        }

        public void Delete(int id)
        {
            CustomerType customerType = _dbContext.CustomerTypes.Find(id);
            if (customerType != null)
                _dbContext.CustomerTypes.Remove(customerType);
        }
    }
}
