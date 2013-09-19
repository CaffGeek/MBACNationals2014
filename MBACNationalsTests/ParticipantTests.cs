using Edument.CQRS;
using Events.Participant;
using MBACNationals.Participant;
using MBACNationals.Participant.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MBACNationalsTests
{
    [TestClass]
    public class ParticipantTests : BDDTest<ParticipantCommandHandlers, ParticipantAggregate>
    {
        private Guid testId;
        private string name;
        private string newName;

        [TestInitialize]
        public void Setup()
        {
            testId = Guid.NewGuid();
            name = "John";
            newName = "David";
        }

        [TestMethod]
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

        [TestMethod]
        public void CanNotDuplicateParticipant()
        {
            Test(
                Given(new ParticipantCreated
                {
                    Id = testId,
                    Name = name
                }),
                When(new CreateParticipant
                {
                    Id = testId,
                    Name = name
                }),
                ThenFailWith<ParticipantAlreadyExists>());
        }

        [TestMethod]
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
