using LearnHibernate.Entity;
using NHibernate.Mapping.ByCode.Conformist;

namespace LearnHibernate.Persistence.Mappings
{
    #region  Table per class hierarchy strategy
    //public class SeasonTicketLoanMapping : SubclassMapping<SeasonTicketLoan>
    //{
    //    public SeasonTicketLoanMapping()
    //    {
    //        DiscriminatorValue("STL");
    //        Property(l => l.Amount, mapper => {
    //            mapper.Column("loan_amount");
    //        });

    //        Property(l => l.StartDate, mapper => {
    //            mapper.Column("loan_start_date");
    //        });

    //        Property(l => l.EndDate, mapper => {
    //            mapper.Column("loan_end_date");
    //        });

    //        Property(l => l.Emi, mapper => {
    //            mapper.Column("loan_emi");
    //        });
    //    }
    //}

    #endregion

    //Table per subClass
    public class SeasonTicketLoanMapping : JoinedSubclassMapping<SeasonTicketLoan>
    {
        public SeasonTicketLoanMapping()
        {
            Schema("LearnNH");
            Table("loan_benefit");

            Key(stl => stl.Column("loan_id"));
            Property(l => l.Amount, mapper => {
                mapper.Column("loan_amount");
            });

            Property(l => l.StartDate, mapper => {
                mapper.Column("loan_start_date");
            });

            Property(l => l.EndDate, mapper => {
                mapper.Column("loan_end_date");
            });

            Property(l => l.Emi, mapper => {
                mapper.Column("loan_emi");
            });
        }
    }
}
