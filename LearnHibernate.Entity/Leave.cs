namespace LearnHibernate.Entity
{

    public class Leave : Benefit
    {
        public virtual int AvailableEntitlement { get; set; }
        public virtual int RemainingEntitlement { get; set; }
        public virtual LeaveType Type { get; set; }
    }
}