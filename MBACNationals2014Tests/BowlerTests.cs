using Edument.CQRS;
using Events.Bowler;
using MBAC.Bowler;
using NUnit.Framework;
using System;

namespace MBACNationals2014Tests
{
    [TestFixture]
    public class BowlerTests : BDDTest<BowlerCommandHandlers, BowlerAggregate>
    {
        private Guid testId;
        private string firstName;

        [SetUp]
        public void Setup()
        {
            testId = Guid.NewGuid();
            firstName = "John";
        }

        [Test]
        public void CanCreateBowler()
        {
            Test(
                Given(),
                When(new CreateBowler
                {
                    Id = testId,
                    FirstName = firstName
                }),
                Then(new BowlerCreated
                {
                    Id = testId,
                    FirstName = firstName
                }));
        }

        [Test]
        public void CanCreateBowlerOnlyOnce()
        {
            Test(
                Given(new BowlerCreated
                {
                    Id = testId,
                    FirstName = firstName
                }),
                When(new CreateBowler
                {
                    Id = testId,
                    FirstName = firstName
                }),
                ThenFailWith<BowlerAlreadyExists>());
        }
    }
}
