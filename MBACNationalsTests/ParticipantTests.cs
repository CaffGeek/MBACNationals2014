using Edument.CQRS;
using Events.Participant;
using MBACNationals.Participant;
using NUnit.Framework;
using System;

namespace MBACNationalsTests
{
    [TestFixture]
    public class ParticipantTests : BDDTest<ParticipantCommandHandlers, ParticipantAggregate>
    {
        private Guid testId;
        private string firstName;
        private string newFirstName;

        [SetUp]
        public void Setup()
        {
            testId = Guid.NewGuid();
            firstName = "John";
            newFirstName = "David";
        }

        [Test]
        public void CanCreateParticipant()
        {
            Test(
                Given(),
                When(new CreateParticipant
                {
                    Id = testId,
                    FirstName = firstName
                }),
                Then(new ParticipantCreated
                {
                    Id = testId,
                    FirstName = firstName
                }));
        }

        [Test]
        public void CanRenameParticipant()
        {
            Test(
                Given(new ParticipantCreated
                {
                    Id = testId,
                    FirstName = firstName
                }),
                When(new RenameParticipant
                {
                    Id = testId,
                    FirstName = newFirstName
                }),
                Then(new ParticipantRenamed
                {
                    Id = testId,
                    FirstName = newFirstName
                }));
        }
    }
}
