(function () {
    "use strict";

    var arrivalsController = function ($scope, $http, $q, $location, modalFactory, dataService) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var province = url.slice(lastSlash+1);

        var emptyArrival = { ArriveAt: '2014-06-28T09:00' };
        var emptyDeparture = { DepartAt: '2014-07-03T09:00' };

        $scope.model = {
            arrivals: [angular.copy(emptyArrival), angular.copy(emptyArrival), angular.copy(emptyArrival)],
            departures: [angular.copy(emptyDeparture), angular.copy(emptyDeparture), angular.copy(emptyDeparture)]
        };

        if (province) {
            //dataService.LoadParticipants(province).
            //    success(function (participants) {
            //        $scope.model.participants = participants;
            //    });
        }

        $scope.addArrival = function () {
            $scope.model.arrivals.push(angular.copy(emptyArrival));
        };

        $scope.addDeparture = function () {
            $scope.model.departures.push(angular.copy(emptyDeparture));
        };

        $scope.saveTravelInfo = function () {
            alert('TODO:');
        };

        $scope.removeRecord = function () {
            alert('TODO:');
        };
    };

    app.controller("ArrivalsController", ["$scope", "$http", "$q", "$location", "modalFactory", "dataService", arrivalsController]);
}());