(function () {
    "use strict";

    var dataService = function ($http) {
        return {
            SaveTeam: saveTeam,
            SaveParticipant: saveParticipant,
            AssignParticipantToTeam: assignParticipantToTeam,
            AssignCoachToTeam: assignCoachToTeam,
            LoadContingent: loadContingent,
            LoadParticipant: loadParticipant,
            LoadParticipants: loadParticipants,
            AssignParticipantToRoom: assignParticipantToRoom,
            RemoveParticipantFromRoom: removeParticipantFromRoom
        };

        function saveTeam(team, contingent) {
            //TODO: Use extend
            return $http.post('/Contingent/CreateTeam', {
                ContingentId: contingent.Id,
                TeamId: team.Id,
                Name: team.Name,
                Gender: team.Gender,
                SizeLimit: team.SizeLimit,
                RequiresShirtSize: team.RequiresShirtSize,
                RequiresCoach: team.RequiresCoach,
                RequiresAverage: team.RequiresAverage,
                RequiresBio: team.RequiresBio,
                RequiresGender: team.RequiresGender,
                IncludesSinglesRep: team.IncludesSinglesRep
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

        function assignCoachToTeam(participant, team) {
            return $http.post('/Contingent/AssignCoachToTeam', {
                Id: participant.Id,
                TeamId: team.Id
            });
        }

        function loadContingent(province) {
            return $http.get('/Contingent', {
                params: { province: province }
            });
        };

        function loadParticipant(id) {
            return $http.get('/Participant', {
                params: { id: id }
            });
        };

        function loadParticipants(province) {
            return $http.get('/Participant/Contingent', {
                params: { province: province }
            });
        };

        function assignParticipantToRoom(id, roomNumber) {
            return $http.post('/Participant/AssignToRoom', {
                Id: id,
                RoomNumber: roomNumber
            });
        }

        function removeParticipantFromRoom(id) {
            return $http.post('/Participant/RemoveFromRoom', {
                Id: id
            });
        }
    };

    app.factory('dataService', ['$http', dataService]);
}());