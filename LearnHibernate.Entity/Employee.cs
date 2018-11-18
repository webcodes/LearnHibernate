namespace LearnHibernate.Entity
{
    using System;
    using System.Collections.Generic;

    public class Employee : Entity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual Address MailingAddress { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual bool IsAdmin { get; set; }
        public virtual ICollection<Benefit> Benefits { get; set; }

        public virtual ICollection<Community> Communities { get; set; }
    }
}
