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

        $scope.handleDrop = function (id, roomNumber) {
            //TODO:!!!
            alert('Participant ' + id + ' has been assigned to room #' + roomNumber);
        }
    };

    app.controller("ReservationController", ["$scope", "$http", "$q", "$location", "modalFactory", "dataService", reservationController]);
}());