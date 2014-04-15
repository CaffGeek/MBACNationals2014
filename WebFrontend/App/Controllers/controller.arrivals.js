(function () {
    "use strict";

    var arrivalsController = function ($scope, $http, $q, $location, modalFactory, dataService) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var province = url.slice(lastSlash+1);

        $scope.model = {
            arrivals: [{}],
            departures: [{}]
        };

        if (province) {
            //dataService.LoadParticipants(province).
            //    success(function (participants) {
            //        $scope.model.participants = participants;
            //    });
        }
    };

    app.controller("ArrivalsController", ["$scope", "$http", "$q", "$location", "modalFactory", "dataService", arrivalsController]);
}());