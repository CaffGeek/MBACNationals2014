(function () {
    "use strict";

    var dataService = function ($http) {
        return {
            SaveTeam: saveTeam,
            SaveParticipant: saveParticipant,
            AssignParticipantToTeam: assignParticipantToTeam
        };

        function saveTeam(team) {
            return $http.post('/Team/Create', team);
        }

        function saveParticipant(participant) {
            return $http.post('/Participant/Create', participant);
        }

        function assignParticipantToTeam(participant, team) {
            return $http.post('/Contingent/AssignParticipantToTeam', {
                Id: participant.Id,
                TeamId: team.Id
            });
        }
    };

    app.factory('dataService', ['$http', dataService]);
}());