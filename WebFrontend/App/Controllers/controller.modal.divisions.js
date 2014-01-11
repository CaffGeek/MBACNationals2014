(function () {
    "use strict";

    var modalDivisionsController = function ($scope, $modalInstance, $http, title, divisions) {
        $scope.model = {};

        $scope.model.title = title;
        $scope.model.divisions = [ //TODO: setup selected divisions
            { Name: 'Tournament Men Single', Selected: true, Gender: 'M', SizeLimit: 1, RequiresShirtSize: true, RequiresBio: true },
            { Name: 'Tournament Ladies Single', Selected: true, Gender: 'F', SizeLimit: 1, RequiresShirtSize: true, RequiresBio: true },
            { Name: 'Tournament Men', Selected: true, Gender: 'M', SizeLimit: 5, RequiresCoach: true },
            { Name: 'Tournament Ladies', Selected: true, Gender: 'F', SizeLimit: 5, RequiresCoach: true },
            { Name: 'Teaching Men', Selected: true, Gender: 'M', SizeLimit: 5, RequiresCoach: true, IncludesSinglesRep: true, RequiresAverage: true },
            { Name: 'Teaching Ladies', Selected: true, Gender: 'F', SizeLimit: 5, RequiresCoach: true, IncludesSinglesRep: true, RequiresAverage: true },
            { Name: 'Seniors', Selected: true, SizeLimit: 5, RequiresCoach: true, RequiresAverage: true, RequiresGender: true }
        ];

        if (divisions.length) {
            angular.forEach($scope.model.divisions, function (value) {
                var division = divisions.filter(function (obj) { return obj.Name === value.Name; });
                if (!division.length) {
                    value.Selected = false;
                }
            });
        }

        $scope.save = function () {
            $modalInstance.close($scope.model.divisions);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    };

    app.controller("ModalDivisionsController", modalDivisionsController);
}());