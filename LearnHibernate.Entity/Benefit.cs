namespace LearnHibernate.Entity
{
    public abstract class Benefit : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public virtual Employee Employee { get; set; }
    }
}