using FluentNHibernate.Mapping;
using LearnHibernate.Entity;

namespace LearnHibernate.Persistence.Mappings.FNH
{
    public class CommunityFNHMapping : ClassMap<Community>
    {
        public CommunityFNHMapping()
        {
            Schema("LearnNH");

            Id(c => c.Id)
                .GeneratedBy.HiLo("1000")
                .Column("community_id");

            Map(e => e.Name);

            Map(e => e.Description)
                .Column("desc");

            HasManyToMany(c => c.Employees)
                .Table("employee_community")
                .ParentKeyColumn("community_id")
                .ChildKeyColumn("employee_id")
                .Cascade.AllDeleteOrphan()
                .Inverse();
        }
    }
}
