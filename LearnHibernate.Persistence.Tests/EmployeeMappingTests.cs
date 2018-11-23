using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnHibernate.Entity;
using NHibernate;
using Xunit;

namespace LearnHibernate.Persistence.Tests
{
    public class EmployeeMappingTests
    {

        public EmployeeMappingTests()
        {
            var database = new InMemoryMappingDatabase();
            session = database.Session;
        }

        [Fact]
        public async Task Employee_PrimitiveMapping_IsValid()
        {
            object id = 0;

            using (var trans = this.session.BeginTransaction())
            {
                id = await this.session.SaveAsync(GetDummyEmployee());
                await trans.CommitAsync();
            }

            this.session.Clear();

            var dummyEmployee = GetDummyEmployee();
            using (var trans = this.session.BeginTransaction())
            {
                var employee = await this.session.GetAsync<Employee>(id);
                Assert.Equal(dummyEmployee.FirstName, employee.FirstName);
                Assert.Equal(dummyEmployee.LastName, employee.LastName);
                Assert.Equal(dummyEmployee.EmailAddress, employee.EmailAddress);
                Assert.Equal(dummyEmployee.PasswordHash, employee.PasswordHash);
                Assert.True(employee.IsAdmin);
                Assert.Equal(dummyEmployee.StartDate, employee.StartDate.ToUniversalTime().Date);
                Assert.Equal(dummyEmployee.BirthDate, employee.BirthDate.ToUniversalTime().Date);

                await trans.CommitAsync();
            }
        }

        [Fact]
        public async Task Employee_BenefitsMapping_IsValid()
        {
            object id = 0;
            var dummy = GetDummyEmployee();
            using (var trans = this.session.BeginTransaction())
            {
                dummy.Benefits = new HashSet<Benefit> {
                    new Leave
                    {
                        AvailableEntitlement = 20,
                        RemainingEntitlement = 5,
                        Type = LeaveType.Paid
                    },
                    new SeasonTicketLoan
                    {
                        Amount = 240m,
                        Emi = 20m,
                        StartDate = new DateTime(2018, 01, 01),
                        EndDate = new DateTime(2018, 12, 31)
                    },
                    new TrainingAllowance
                    {
                        Entitlement = 500,
                        Remaining = 100

                    }
                };
                id = await this.session.SaveAsync(dummy);
                await trans.CommitAsync();
            }

            this.session.Clear();

            using (var trans = this.session.BeginTransaction())
            {
                var employee = await this.session.GetAsync<Employee>(id);
                Assert.NotNull(employee.Benefits);
                Assert.Equal(3, employee.Benefits.Count);

                var seasonTicketLoan = employee.Benefits.OfType<SeasonTicketLoan>().FirstOrDefault();
                Assert.NotNull(seasonTicketLoan);
                Assert.Equal("Awesome", seasonTicketLoan.Employee.FirstName);

                var trainingBenefit = employee.Benefits.OfType<TrainingAllowance>().FirstOrDefault();
                Assert.NotNull(trainingBenefit);
                Assert.Equal("Awesome", trainingBenefit.Employee.FirstName);

                var leave = employee.Benefits.OfType<Leave>().FirstOrDefault();
                Assert.NotNull(leave);
                Assert.Equal("Awesome", leave.Employee.FirstName);
                await trans.CommitAsync();
            }
        }

        [Fact]
        public async Task Employee_AddressMapping_IsValid()
        {
            object id = 0;
            var dummy = GetDummyEmployee();
            var mailingAdress = new Address
            {
                AddressLine1 = "Line 1",
                AddressLine2 = "Line 2",
                City = "New York",
                State = "NY",
                PostalCode = "10038"
            };

            dummy.MailingAddress = mailingAdress;
            //mailingAdress.Employee = dummy;

            using (var trans= session.BeginTransaction())
            {
                id = await session.SaveAsync(dummy);
                await trans.CommitAsync();
            }

            session.Clear();

            using (var trans = session.BeginTransaction())
            {
                var employee = await session.GetAsync<Employee>(id);
                Assert.Equal("Line 1", employee.MailingAddress.AddressLine1);
                Assert.Equal("Line 2", employee.MailingAddress.AddressLine2);
                //Assert.Equal("Awesome", employee.MailingAddress.Employee.FirstName);
                await trans.CommitAsync();
            }
        }

        [Fact]
        public async Task Employee_CommunitiesMapping_IsValid()
        {
            object id = 0;
            var dummy = GetDummyEmployee();
            var communities = new HashSet<Community>
            {
                new Community { Name ="Community 1" },
                new Community { Name = "Community 2", Description = "Training Community" }
            };

            using (var trans = session.BeginTransaction())
            {
                dummy.Communities = communities;
                id = await session.SaveAsync(dummy);
                await trans.CommitAsync();
            }

            session.Clear();

            using (var trans = session.BeginTransaction())
            {
                var employee = await session.GetAsync<Employee>(id);
                Assert.Equal(2, employee.Communities.Count);
                Assert.Equal("Community 1", employee.Communities.First().Name);
                Assert.Equal("Training Community", employee.Communities.Skip(1).First().Description);
                Assert.Equal("Awesome", employee.Communities.First().Employees.First().FirstName);
                await trans.CommitAsync();
            }
        }

        private static Employee GetDummyEmployee()
        {
            var birthDate = DateTime.UtcNow.AddYears(-30).Date;
            var startDate = DateTime.UtcNow.AddYears(-1).Date;
            return new Employee
            {
                FirstName = "Awesome",
                LastName = "Developer",
                EmailAddress = "awesome@developer.com",
                IsAdmin = true,
                PasswordHash = "secret",
                StartDate = startDate,
                BirthDate = birthDate
            };
        }

        private readonly ISession session;
    }
}
