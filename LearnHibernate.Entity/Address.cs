namespace LearnHibernate.Entity
{
    public class Address /*: Entity*/
    {
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string PostalCode { get; set; }

        //public virtual Employee Employee { get; set; }
    }
}