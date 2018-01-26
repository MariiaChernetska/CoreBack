using System;
using System.Collections.Generic;
using System.Text;

namespace PillarInterview.Services.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Comments { get; set; }
        public int? NumberOfSchools { get; set; }
        public string Type { get; set; }
    }
}
