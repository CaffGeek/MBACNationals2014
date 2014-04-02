(function () {
    "use strict";

    var dataService = function ($http) {
        return {
            SaveTeam: saveTeam,
            SaveParticipant: saveParticipant,
            AssignParticipantToTeam: assignParticipantToTeam,
            LoadContingent: loadContingent
        };

        function saveTeam(team, contingent) {
            return $http.post('/Contingent/CreateTeam', {
                ContingentId: contingent.Id,
                TeamId: team.Id,
                Name: team.Name,
                Gender: team.Gender,
                SizeLimit: team.SizeLimit,
            });
        }

        function saveParticipant(participant) {
            return participant.Id
                ? $http.post('/Participant/Update', participant)
                : $http.post('/Participant/Create', participant);
        }

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