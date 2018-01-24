using System;
using System.Collections.Generic;
using System.Text;

namespace PillarInterview.Services.Models
{
    public class ContactSaveModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
