using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        
        public virtual ICollection<Project> Projects { get; set; }
    }
}
