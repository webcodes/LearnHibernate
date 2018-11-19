using LearnHibernate.Entity;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace LearnHibernate.Persistence.Mappings
{
    public class EmployeeMapping : ClassMapping<Employee>
    {
        public EmployeeMapping()
        {
            Schema("LearnNH");
            Id(e => e.Id, mapper => {
                mapper.Generator(Generators.HighLow);
                mapper.Column("employee_id");
            });
            Property(e => e.FirstName, mapper => {
                mapper.Column("first_name");
                mapper.NotNullable(true);
                mapper.Length(20);
            });

            Property(e => e.LastName, mapper => {
                mapper.Column("last_name");
                mapper.NotNullable(true);
                mapper.Length(20);
            });

            Property(e => e.EmailAddress, mapper => {
                mapper.Column("email_address");
                mapper.Length(50);
            });

            Property(e => e.BirthDate, mapper => {
                mapper.Column("birth_date");
                mapper.NotNullable(true);
            });

            Property(e => e.StartDate, mapper => {
                mapper.Column("start_date");
                mapper.NotNullable(true);
            });

            Property(e => e.PasswordHash, mapper => {
                mapper.Column("password_hash");
                mapper.NotNullable(true);
                mapper.Length(20);
            });

            Property(e => e.IsAdmin, mapper => {
                mapper.Column("admin");
            });

            Set(e => e.Benefits, 
                mapper => {
                    mapper.Key(k => k.Column("employee_id"));
                    mapper.Cascade(Cascade.DeleteOrphans);
                }, 
                relation => relation.OneToMany(mapping => mapping.Class(typeof(Benefit))));

            //Had address been an entity instead of component
            //OneToOne(e => e.MailingAddress, mapper => {
            //    mapper.PropertyReference(a => a.Employee);
            //    mapper.Cascade(Cascade.All);
            //});

            Set(e => e.Communities,
                mapper => {
                    mapper.Key(k => k.Column("employee_id"));
                    mapper.Table("lnh_employee_community");
                    mapper.Cascade(Cascade.DeleteOrphans);
                },
                relation => relation.ManyToMany(mapper => {
                    mapper.Column("community_id");
                    mapper.Class(typeof(Community));
                }));

            Component(e => e.MailingAddress, mapper => {
                mapper.Property(a => a.AddressLine1, adrmap => adrmap.Column("address_line1"));
                mapper.Property(a => a.AddressLine2, adrmap => adrmap.Column("address_line2"));
                mapper.Property(a => a.City);
                mapper.Property(a => a.State);
                mapper.Property(a => a.PostalCode, adrmap => adrmap.Column("postal_code"));
            });
        }
    }
}
