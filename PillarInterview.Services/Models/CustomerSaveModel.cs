using PillarInterview.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PillarInterview.Services.Models
{
    public class CustomerSaveModelBase {
        


    }
    public class CustomerSaveModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Commnents { get; set; }
        public int? NumberOfSchools { get; set; }
        public int Type { get; set; }
        public UserSaveModel[] Users { get; set; }
        public DepartmentSaveModel[] Departments { get; set; }
        public ContactSaveModel[] Contacts { get; set; }

    }
}
