using LearnHibernate.Entity;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace LearnHibernate.Persistence.Mappings
{
    public class CommunityMapping : ClassMapping<Community>
    {
        public CommunityMapping()
        {
            Schema("LearnNH");

            Id(c => c.Id, mapper => {
                mapper.Generator(Generators.HighLow);
                mapper.Column("community_id");
            });

            Property(e => e.Name);

            Property(e => e.Description, mapper => {
                mapper.Column("desc");
            });

            Set(c => c.Employees, mapper => {
                mapper.Table("employee_community");
                mapper.Cascade(Cascade.All|Cascade.DeleteOrphans);
                mapper.Inverse(true);
                mapper.Key(k => k.Column("community_id"));
            },
            relation => relation.ManyToMany(mapper => {
                mapper.Class(typeof(Employee));
                mapper.Column("employee_id");
            }));

        }
    }
}
