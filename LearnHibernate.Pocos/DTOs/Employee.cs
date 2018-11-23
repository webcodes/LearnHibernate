using System;
using System.ComponentModel.DataAnnotations;
namespace LearnHibernate.Pocos.DTOs
{
    public class Employee
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string EmailAddress { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public bool IsAdmin { get; set; }
    }
}
