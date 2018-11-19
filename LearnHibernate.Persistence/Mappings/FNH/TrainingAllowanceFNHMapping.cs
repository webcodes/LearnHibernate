using FluentNHibernate.Mapping;
using LearnHibernate.Entity;

namespace LearnHibernate.Persistence.Mappings.FNH
{
    #region  Table per class hierarchy strategy
    //public class TrainingAllowanceFNHMapping : SubclassMap<TrainingAllowance>
    //{
    //    public TrainingAllowanceFNHMapping()
    //    {
    //        DiscriminatorValue("TRA");
    //        Map(l => l.Entitlement)
    //            .Column("training_available");

    //        Map(l => l.Remaining)
    //            .Column("training_remaining");
    //    }
    //}
    #endregion

    //Table per subClass

    public class TrainingAllowanceFNHMapping : SubclassMap<TrainingAllowance>
    {
        public TrainingAllowanceFNHMapping()
        {
            Schema("LearnNH");
            Table("training_benefit");
            KeyColumn("training_allowance_id");
            Map(l => l.Entitlement)
                .Column("training_available");

            Map(l => l.Remaining)
                .Column("training_remaining");
        }
    }
}
