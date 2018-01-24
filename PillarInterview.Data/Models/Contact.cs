using System;
using System.Collections.Generic;
using System.Text;

namespace PillarInterview.Data.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

    }
}
