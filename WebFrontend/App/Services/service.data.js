(function () {
    "use strict";

    var dataService = function ($http) {
        return {
            SaveTeam: saveTeam,
            SaveParticipant: saveParticipant,
            //AssignTeamToContingent: assignTeamToContingent,
            AssignParticipantToTeam: assignParticipantToTeam,
            LoadContingent: loadContingent
        };

        function saveTeam(team, contingent) {
            return $http.post('/Contingent/CreateTeam', {
                ContingentId: contingent.Id,
                TeamId: team.Id,
                Name: team.Name,
            });
        }

        function saveParticipant(participant) {
            return $http.post('/Participant/Create', participant);
        }

        //function assignTeamToContingent(team, contingent) {
        //    return $http.post('/Contingent/AssignTeamToContingent', {
        //        Id: team.Id,
        //        ContingentId: contingent.Id
        //    });
        //}

        function assignParticipantToTeam(participant, team) {
            return $http.post('/Contingent/AssignParticipantToTeam', {
                Id: participant.Id,
                TeamId: team.Id
            });
        }

        function loadContingent(province) {
            return $http.get('/Contingent', {
                params: { province: province }
            });
        };
    };

    app.factory('dataService', ['$http', dataService]);
}());