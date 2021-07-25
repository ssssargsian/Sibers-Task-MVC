
using System.ComponentModel.DataAnnotations;

namespace TestSibers.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
    }
}