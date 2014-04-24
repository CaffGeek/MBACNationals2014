(function () {
    "use strict";

    var practiceController = function ($scope, $http, $q, $location, dataService) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var province = url.slice(lastSlash+1);

        $scope.model = {
            province: province,
            allowedTimes: [9, 10, 11, 12, 13, 14, 15],
            teams: [{ Name: 'one' }, { Name: 'two' }, { Name: 'three' }],
            practiceLocations: ['Rossmere', 'Academy', 'Coronation']
        };

        //if (province) {
        //    dataService.LoadPracticePlan(province).
        //        success(function (contingentPracticePlan) {
        //            $scope.model.id = contingentPracticePlan.Id;
        //            $scope.model.province = contingentPracticePlan.Province;
        //            $scope.model.schedule = contingentPracticePlan.Schedule;
        //        });
        //}

    };

    app.controller("PracticeController", ["$scope", "$http", "$q", "$location", "dataService", practiceController]);
}());