namespace LearnHibernate.Entity
{

    public class TrainingAllowance : Benefit
    {
        public int Entitlement { get; set; }
        public int Remaining { get; set; }
    }
}