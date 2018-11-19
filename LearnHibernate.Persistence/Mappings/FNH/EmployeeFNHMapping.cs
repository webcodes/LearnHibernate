using FluentNHibernate.Mapping;
using LearnHibernate.Entity;

namespace LearnHibernate.Persistence.Mappings.FNH
{
    public class EmployeeFNHMapping : ClassMap<Employee>
    {
        public EmployeeFNHMapping()
        {
            Schema("LearnNH");

            Id(e => e.Id).Column("employee_id").GeneratedBy.HiLo("1000");
            Map(e => e.FirstName)
                .Column("first_name").Not.Nullable()
                .Length(20);

            Map(e => e.LastName)
                .Column("last_name")
                .Not.Nullable()
                .Length(20);

            Map(e => e.EmailAddress)
                .Column("email_address")
                .Length(50);

            Map(e => e.BirthDate)
                .Column("birth_date")
                .Not.Nullable();

            Map(e => e.StartDate)
                .Column("start_date")
                .Not.Nullable();

            Map(e => e.PasswordHash)
                .Column("password_hash")
                .Not.Nullable()
                .Length(20);

            Map(e => e.IsAdmin)
                .Column("admin");

            HasMany(e => e.Benefits)
                .KeyColumn("employee_id")
                .Cascade.AllDeleteOrphan();

            //Had address been an entity instead of component
            //HasOne(e => e.MailingAddress)
            //    .PropertyRef(a => a.Employee)
            //    .Cascade.All();

            HasManyToMany(e => e.Communities)
                .ParentKeyColumn("employee_id")
                .Table("employee_community")
                .ChildKeyColumn("community_id")
                    .Cascade.AllDeleteOrphan();

            Component(e => e.MailingAddress, mapper =>
            {
                mapper.Map(a => a.AddressLine1).Column("address_line1");
                mapper.Map(a => a.AddressLine2).Column("address_line2");
                mapper.Map(a => a.City);
                mapper.Map(a => a.State);
                mapper.Map(a => a.PostalCode).Column("postal_code");
            });
        }
    }
}
