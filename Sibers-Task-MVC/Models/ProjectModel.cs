using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestSibers.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Название проекта")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Название компании-заказчика")]
        public string CompanyCustomer { get; set; }
        [Required]
        [Display(Name = "Название компании-исполнителя")]
        public string CompanyExecutor { get; set; }
        [Required]
        [Display(Name = "Приоритет")]
        [Range(0, 10, ErrorMessage = "Недопустимый приоритет. Введите число от 0 до 10")]
        public int Priority { get; set; }
        [Required]
        [Display(Name = "Описание")]
        public string Caption { get; set; }

        public string FullNameProjectManager { get; set; }
        [Required]
        [Display(Name = "Руководитель проекта")]
        public int? ProjectManagerId { get; set; }
        //public string ProjectManagerIdString { get; set; }
        public EmployeeModel ProjectManager { get; set; }
        [Required]
        [Display(Name = "Дата начала")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "Дата окончания")]
        public DateTime EndDate { get; set; }
    }


}