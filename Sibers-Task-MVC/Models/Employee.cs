using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sibers_Task_MVC.Models
{
    public class Employee
    {

        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Please first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please last name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please last name")]
        public string Patronymic { get; set; }
        public string Email { get; set; }

       
    }
}
