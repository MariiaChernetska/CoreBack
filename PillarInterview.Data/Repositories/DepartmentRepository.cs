using PillarInterview.Data.IRepositories;
using PillarInterview.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillarInterview.Data.Repositories
{
    public class DepartmentRepository : IRepository<Department>
    {
        ApplicationDbContext _dbContext;
        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Department> Get()
        {
            return _dbContext.Departments;
        }

        public Department Get(int id)
        {
            return _dbContext.Departments.Find(id);
        }

        public Department Create(Department department)
        {
            var saved = _dbContext.Departments.Add(department);
            return saved.Entity;
        }

        public Department Update(Department department)
        {
            var saved = _dbContext.Update(department);
            return saved.Entity;
        }

        public void Delete(int id)
        {
            Department department = _dbContext.Departments.Find(id);
            if (department != null)
                _dbContext.Departments.Remove(department);
        }
    }
}
