namespace LearnHibernate.Entity
{

    public class TrainingAllowance : Benefit
    {
        public virtual int Entitlement { get; set; }
        public virtual int Remaining { get; set; }
    }
}