namespace LearnHibernate.Entity
{

    public class Leave : Benefit
    {
        public int Entitlement { get; set; }
        public int Remaining { get; set; }
        public LeaveType Type { get; set; }
    }
}