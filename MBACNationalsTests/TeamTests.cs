using Edument.CQRS;
using Events;
using Events.Team;
using MBACNationals.Team;
using MBACNationals.Team.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MBACNationalsTests
{
    [TestClass]
    public class TeamTests : BDDTest<TeamCommandHandlers, TeamAggregate>
    {
        private Guid testId;
        private List<TeamMember> teamMembers = new List<TeamMember> { new TeamMember { ParticipantId = Guid.NewGuid() } };

        [TestInitialize]
        public void Setup()
        {
            testId = Guid.NewGuid();
        }

        [TestMethod]
        public void CanCreateTeam()
        {
            Test(
                Given(),
                When(new CreateTeam
                {
                    Id = testId
                }),
                Then(new TeamCreated
                {
                    Id = testId
                }));
        }

        [TestMethod]
        public void CanNotDuplicateTeam()
        {
            Test(
                Given(new TeamCreated
                {
                    Id = testId
                }),
                When(new CreateTeam
                {
                    Id = testId
                }),
                ThenFailWith<TeamAlreadyExists>());
        }

        [TestMethod]
        public void CanAssignParticipantToTeam()
        {
            Test(
                Given(new TeamCreated
                {
                    Id = testId
                }),
                When(new AssignTeamMembers
                {
                    Id = testId,
                    Members = teamMembers
                }),
                Then(new TeamMembersAssigned
                {
                    Members = teamMembers
                }));
        }
    }
}
