using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearnHibernate.Entity;
using NHibernate;
using Xunit;

namespace LearnHibernate.Persistence.Tests
{
    public class BenefitMappingTests
    {
        private readonly InMemoryMappingDatabase database;
        private readonly ISession session;

        public BenefitMappingTests()
        {
            database = new InMemoryMappingDatabase();
            session = database.Session;
        }

        [Fact]
        public async Task BenefitMapping_Works()
        {
            object id = 0;
            using (var trans = session.BeginTransaction())
            {
                id = await session.SaveAsync(new TrainingAllowance {
                    Name = "LearnNH",
                    Description = "Learning nHibernate",
                    Entitlement = 500,
                    Remaining = 10
                });
                await trans.CommitAsync();
            }

            session.Clear();

            using (var trans = session.BeginTransaction())
            {
                var benefit = await session.GetAsync<Benefit>(id);
                var trainingAllowance = benefit as TrainingAllowance;
                Assert.NotNull(trainingAllowance);

                Assert.Equal("LearnNH", trainingAllowance.Name);
                Assert.Equal("Learning nHibernate", trainingAllowance.Description);
                Assert.Equal(500, trainingAllowance.Entitlement);
                Assert.Equal(10, trainingAllowance.Remaining);
                await trans.CommitAsync();
            }
        }
    }
}
