using FluentNHibernate.Mapping;
using LearnHibernate.Entity;

namespace LearnHibernate.Persistence.Mappings.FNH
{
    public class BenefitFNHMapping : ClassMap<Benefit>
    {
        public BenefitFNHMapping()
        {
            Schema("LearnNH");
            Id(b => b.Id)
                .Column("benefit_id")
                .GeneratedBy.HiLo("1000");

            Map(e => e.Name);

            Map(e => e.Description).Column("desc");

            References(b => b.Employee)
                .Column("employee_id");
        }
    }
}
