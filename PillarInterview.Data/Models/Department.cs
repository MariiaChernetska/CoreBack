using System.Collections.Generic;

namespace PillarInterview.Data.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        
        public virtual DepartmentManager DepartmentManager { get; set; }

        public virtual List<UserInfo> Users { get; set; }
    }
}