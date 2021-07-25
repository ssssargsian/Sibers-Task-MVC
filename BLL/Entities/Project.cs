using System;
using System.Collections.Generic;

namespace Repository.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyCustomer { get; set; }
        public string CompanyExecutor { get; set; }
        public int Priority { get; set; }
        public string Caption { get; set; }
        
        public int? ProjectManagerId { get; set; }
        public Employee ProjectManager { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
