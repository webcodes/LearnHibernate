namespace LearnHibernate.Pocos.Command.Employee
{
    using LearnHibernate.Pocos.DTOs;
    using LearnHibernate.Pocos.Validators;
    using System.ComponentModel.DataAnnotations;

    public class AddEmployeeCommand
    {
        [Required, ValidateObject]
        public Employee Employee { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
