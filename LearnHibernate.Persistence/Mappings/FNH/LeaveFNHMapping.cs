using FluentNHibernate.Mapping;
using LearnHibernate.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearnHibernate.Persistence.Mappings.FNH
{
    #region Table per class hierarchy strategy
    //public class LeaveFNHMapping : SubclassMap<Leave>
    //{
    //    public LeaveFNHMapping()
    //    {
    //        DiscriminatorValue("LVE");
    //        Map(l => l.AvailableEntitlement).Column("leave_available");

    //        Map(l => l.RemainingEntitlement)
    //            .Column("leave_remaining");

    //        Map(l => l.Type)
    //            .Column("leave_type");
    //    }
    //}

    #endregion

    //Table per subClass
    public class LeaveFNHMapping : SubclassMap<Leave>
    {
        public LeaveFNHMapping()
        {

            Schema("LearnNH");
            Table("leave_benefit");

            KeyColumn("leave_id");
            Map(l => l.AvailableEntitlement)
                .Column("leave_available");

            Map(l => l.RemainingEntitlement)
                .Column("leave_remaining");

            Map(l => l.Type)
                .Column("leave_type");
        }
    }
}
