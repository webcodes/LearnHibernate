using FluentNHibernate.Mapping;
using LearnHibernate.Entity;

namespace LearnHibernate.Persistence.Mappings.FNH
{
    #region  Table per class hierarchy strategy
    //public class SeasonTicketLoanFNHMapping : SubclassMap<SeasonTicketLoan>
    //{
    //    public SeasonTicketLoanFNHMapping()
    //    {
    //        DiscriminatorValue("STL");
    //        Map(l => l.Amount)
    //            .Column("loan_amount");

    //        Map(l => l.StartDate)
    //            .Column("loan_start_date");

    //        Map(l => l.EndDate)
    //            .Column("loan_end_date");

    //        Map(l => l.Emi)
    //            .Column("loan_emi");
    //    }
    //}

    #endregion

    public class SeasonTicketLoanFNHMapping : SubclassMap<SeasonTicketLoan>
    {
        public SeasonTicketLoanFNHMapping()
        {
            Schema("LearnNH");
            Table("loan_benefit");

            KeyColumn("loan_id");
            Map(l => l.Amount)
                .Column("loan_amount");

            Map(l => l.StartDate)
                .Column("loan_start_date");

            Map(l => l.EndDate)
                .Column("loan_end_date");

            Map(l => l.Emi)
                .Column("loan_emi");
        }
    }

}
