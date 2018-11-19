using LearnHibernate.Entity;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace LearnHibernate.Persistence.Mappings
{
    public class BenefitMapping : ClassMapping<Benefit>
    {
        public BenefitMapping()
        {
            Schema("LearnNH");
            Id(b => b.Id, mapper => {
                mapper.Generator(Generators.HighLow);
                mapper.Column("benefit_id");
            });

            Property(e => e.Name);

            Property(e => e.Description, mapper => {
                mapper.Column("desc");
            });

            ManyToOne(b => b.Employee, mapper => {
                mapper.Class(typeof(Employee));
                mapper.Column("employee_id");
            });
        }
    }
}
