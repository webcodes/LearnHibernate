using System;

namespace LearnHibernate.Entity
{
    public class SeasonTicketLoan : Benefit
    {
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Emi { get; set; }
    }
}