namespace LearnHibernate.Entity
{
    using System;
    using System.Collections.Generic;

    public class Community : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
