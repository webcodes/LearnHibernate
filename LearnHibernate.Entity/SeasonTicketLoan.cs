using System;

namespace LearnHibernate.Entity
{
    public class SeasonTicketLoan : Benefit
    {
        public virtual decimal Amount { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual decimal Emi { get; set; }
    }
}