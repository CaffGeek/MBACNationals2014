using Events.Participant;
using MBACNationalsReadModels;
using System.Collections.Generic;

namespace MBACNationals.ReadModels
{
    public interface ITeamQueries
    {
        List<Teams.Team> GetTeams();

        Teams.Team GetTeam(System.Guid id);
    }
}
