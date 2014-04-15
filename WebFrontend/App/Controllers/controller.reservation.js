(function () {
    "use strict";

    var reservationController = function ($scope, $http, $q, $location, modalFactory, dataService) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var province = url.slice(lastSlash+1);

        $scope.model = {
            participants: []
        };

        if (province) {
            dataService.LoadParticipants(province).
                success(function (participants) {
                    $scope.model.participants = participants;
                });
        }

        $scope.addToRoom = function (id, roomNumber) {
            dataService.AssignParticipantToRoom(id, roomNumber).then(function (data) {
                var participant = $scope.model.participants.filter(function (obj) { return obj.Id == id; })[0];
                if (!participant)
                    return;

                participant.RoomNumber = roomNumber;
            });
        }

        $scope.removeFromRoom = function (id) {
            dataService.RemoveParticipantFromRoom(id).then(function (data) {
                var participant = $scope.model.participants.filter(function (obj) { return obj.Id == id; })[0];
                if (!participant)
                    return;

                participant.RoomNumber = 0;
            });
        }
    };

    app.controller("ReservationController", ["$scope", "$http", "$q", "$location", "modalFactory", "dataService", reservationController]);
}());