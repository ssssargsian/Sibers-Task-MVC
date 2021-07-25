using System;

namespace Services.Entities
{
    public class ProjectServiceEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyCustomer { get; set; }
        public string CompanyExecutor { get; set; }
        public int Priority { get; set; }
        public string Caption { get; set; }
        public string FullNameProjectManager { get; set; }

        public int? ProjectManagerId { get; set; }
        public EmployeeServiceEntity ProjectManager { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public enum SortProject
    {
        Id,
        Name,
        Company,
        Executor,
        FullNameProjectManager,
        Priority,
        StartDate,
        EndDate
    }
}
