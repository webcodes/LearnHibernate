using LearnHibernate.Entity;
using NHibernate.Mapping.ByCode.Conformist;

namespace LearnHibernate.Persistence.Mappings
{
    #region Table per class hierarchy strategy
    //public class LeaveMapping : SubclassMapping<Leave>
    //{
    //    public LeaveMapping()
    //    {
    //        DiscriminatorValue("LVE");
    //        Property(l => l.AvailableEntitlement, mapper => {
    //            mapper.Column("leave_available");
    //        });

    //        Property(l => l.RemainingEntitlement, mapper => {
    //            mapper.Column("leave_remaining");
    //        });

    //        Property(l => l.Type, mapper => {
    //            mapper.Column("leave_type");
    //        });
    //    }
    //}

    #endregion

    //Table per subClass
    public class LeaveMapping : JoinedSubclassMapping<Leave>
    {
        public LeaveMapping()
        {
            Schema("LearnNH");
            Table("leave_benefit");

            Key(l => l.Column("leave_id"));
            Property(l => l.AvailableEntitlement, mapper => {
                mapper.Column("leave_available");
            });

            Property(l => l.RemainingEntitlement, mapper => {
                mapper.Column("leave_remaining");
            });

            Property(l => l.Type, mapper => {
                mapper.Column("leave_type");
            });
        }
    }
}
