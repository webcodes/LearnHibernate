namespace LearnHibernate.Entity
{
    using System;

    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int EmailAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime StartDate { get; set; }
        public Address Address { get; set; }
        public string PasswordHash { get; set; }
    }
}
