(function () {
    "use strict";

    var arrivalsController = function ($scope, $http, $q, $location, modalFactory, dataService) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var province = url.slice(lastSlash+1);

        var emptyArrival = { When: '2014-06-28T09:00', Type: 1 };
        var emptyDeparture = { When: '2014-07-03T09:00', Type: 2 };

        $scope.model = {
            province: province,
            travelPlans: [
                angular.copy(emptyArrival), angular.copy(emptyArrival), angular.copy(emptyArrival),
                angular.copy(emptyDeparture), angular.copy(emptyDeparture), angular.copy(emptyDeparture)]
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

        $scope.saveTravelPlans = function () {
            dataService.SaveTravelPlans($scope.model);
        };

        $scope.removeRecord = function (travelPlan) {
            var idx = $scope.model.travelPlans.indexOf(travelPlan);
            if (idx < 0)
                return;

            $scope.model.travelPlans.splice(idx, 1);
        };
    };

    app.controller("ArrivalsController", ["$scope", "$http", "$q", "$location", "modalFactory", "dataService", arrivalsController]);
}());