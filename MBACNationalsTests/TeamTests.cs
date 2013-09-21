using Edument.CQRS;
using Events.Team;
using MBACNationals.Team;
using MBACNationals.Team.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MBACNationalsTests
{
    [TestClass]
    public class TeamTests : BDDTest<TeamCommandHandlers, TeamAggregate>
    {
        private Guid teamId;
        private string teamName;

        [TestInitialize]
        public void Setup()
        {
            teamId = Guid.NewGuid();
            teamName = "Test Team";
        }

        [TestMethod]
        public void CanCreateTeam()
        {
            Test(
                Given(),
                When(new CreateTeam
                {
                    Id = teamId,
                    Name = teamName,
                }),
                Then(new TeamCreated
                {
                    Id = teamId,
                    Name = teamName,
                }));
        }

        [TestMethod]
        public void CanNotDuplicateTeam()
        {
            Test(
                Given(new TeamCreated
                {
                    Id = teamId,
                    Name = teamName,
                }),
                When(new CreateTeam
                {
                    Id = teamId,
                    Name = teamName,
                }),
                ThenFailWith<TeamAlreadyExists>());
        }
    }
}
