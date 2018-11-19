using LearnHibernate.Entity;
using NHibernate.Mapping.ByCode.Conformist;

namespace LearnHibernate.Persistence.Mappings
{

    #region  Table per class hierarchy strategy
    //public class TrainingAllowanceMapping : SubclassMapping<TrainingAllowance>
    //{
    //    public TrainingAllowanceMapping()
    //    {
    //        DiscriminatorValue("TRA");
    //        Property(l => l.Entitlement, mapper => {
    //            mapper.Column("training_available");
    //        });

    //        Property(l => l.Remaining, mapper => {
    //            mapper.Column("training_remaining");
    //        });
    //    }
    //}

    #endregion

    //Table per subClass
    public class TrainingAllowanceMapping : JoinedSubclassMapping<TrainingAllowance>
    {
        public TrainingAllowanceMapping()
        {
            Schema("LearnNH");
            Table("training_benefit");
            Key(tra => tra.Column("training_allowance_id"));
            Property(l => l.Entitlement, mapper => {
                mapper.Column("training_available");
            });

            Property(l => l.Remaining, mapper => {
                mapper.Column("training_remaining");
            });
        }
    }
}
