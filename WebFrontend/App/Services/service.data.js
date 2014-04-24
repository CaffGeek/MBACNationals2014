(function () {
    "use strict";

    var dataService = function ($http) {
        return {
            SaveTeam: saveTeam,
            RemoveTeam: removeTeam,
            SaveParticipant: saveParticipant,
            AssignParticipantToContingent: assignParticipantToContingent,
            AssignParticipantToTeam: assignParticipantToTeam,
            AssignCoachToTeam: assignCoachToTeam,
            LoadContingent: loadContingent,
            LoadParticipant: loadParticipant,
            LoadParticipants: loadParticipants,
            AssignParticipantToRoom: assignParticipantToRoom,
            RemoveParticipantFromRoom: removeParticipantFromRoom,
            LoadRooms: loadRooms,
            ChangeRoomType: changeRoomType,
            LoadTravelPlans: loadTravelPlans,
            SaveTravelPlans: saveTravelPlans,
            LoadPracticePlan: loadPracticePlan
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

        function removeTeam(team, contingent) {
            return $http.post('/Contingent/RemoveTeam', {
                ContingentId: contingent.Id,
                TeamId: team.Id
            });
        }

        function saveParticipant(participant) {
            return participant.Id
                ? $http.post('/Participant/Update', participant)
                : $http.post('/Participant/Create', participant);
        }

        function assignParticipantToContingent(participant, contingent) {
            return $http.post('/Contingent/AssignParticipantToContingent', {
                Id: participant.Id,
                ContingentId: contingent.Id
            });
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
        };

        function removeParticipantFromRoom(id) {
            return $http.post('/Participant/RemoveFromRoom', {
                Id: id
            });
        };

        function loadRooms(province) {
            return $http.get('/Contingent/Rooms', {
                params: { province: province }
            });
        };

        function changeRoomType(province, roomNumber, type) {
            return $http.post('/Contingent/ChangeRoomType', {
                Province: province,
                RoomNumber: roomNumber,
                Type: type
            });
        };

        function loadTravelPlans(province) {
            return $http.get('/Contingent/TravelPlans', {
                params: { province: province }
            });
        };

        function saveTravelPlans(travelPlans) {
            return $http.post('/Contingent/SaveTravelPlans', travelPlans);
        };

        function loadPracticePlan(province) {
            return $http.get('/Contingent/PracticePlan', {
                params: { province: province }
            });
        }
    };

    app.factory('dataService', ['$http', dataService]);
}());