using System.Collections.Generic;

namespace PillarInterview.Data.Models
{
    public class CustomerType
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}