using System.Collections.Generic;

namespace PillarInterview.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Commnents { get; set; }
        public int? NumberOfSchools { get; set; }
        public int TypeId { get; set; }
        public virtual CustomerType Type { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<UserInfo> Users { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
