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

        [TestInitialize]
        public void Setup()
        {
            teamId = Guid.NewGuid();
        }

        [TestMethod]
        public void CanCreateTeam()
        {
            Test(
                Given(),
                When(new CreateTeam
                {
                    Id = teamId
                }),
                Then(new TeamCreated
                {
                    Id = teamId
                }));
        }

        [TestMethod]
        public void CanNotDuplicateTeam()
        {
            Test(
                Given(new TeamCreated
                {
                    Id = teamId
                }),
                When(new CreateTeam
                {
                    Id = teamId
                }),
                ThenFailWith<TeamAlreadyExists>());
        }
    }
}
