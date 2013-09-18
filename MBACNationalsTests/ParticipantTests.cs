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
        private string name;
        private string newName;

        [SetUp]
        public void Setup()
        {
            testId = Guid.NewGuid();
            name = "John";
            newName = "David";
        }

        [Test]
        public void CanCreateParticipant()
        {
            Test(
                Given(),
                When(new CreateParticipant
                {
                    Id = testId,
                    Name = name
                }),
                Then(new ParticipantCreated
                {
                    Id = testId,
                    Name = name
                }));
        }

        [Test]
        public void CanRenameParticipant()
        {
            Test(
                Given(new ParticipantCreated
                {
                    Id = testId,
                    Name = name
                }),
                When(new RenameParticipant
                {
                    Id = testId,
                    Name = newName
                }),
                Then(new ParticipantRenamed
                {
                    Id = testId,
                    Name = newName
                }));
        }
    }
}
