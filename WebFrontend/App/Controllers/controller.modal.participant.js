(function () {
    "use strict";

    var modalParticipantController = function ($scope, $q, $modalInstance, $http, dataService, participant, team) {
        $scope.model = {};
        
        $scope.model.title = team.Name;
        $scope.model.participant = participant || {};
        $scope.model.team = team || {};

        $scope.save = function () {
            var deferred = $q.defer();

            $q.all([
                //TODO: The team should be saved when the divisions controller is completed, not now, this is too late
                //dataService.SaveTeam($scope.model.team).then(
                //    function (response) {
                //        $scope.model.team.Id = response.data.Id;
                //    }),
                dataService.SaveParticipant($scope.model.participant).then(
                    function (response) {
                        $scope.model.participant.Id = response.data.Id;
                    })
            ]).then(function (response) {
                dataService.AssignParticipantToTeam($scope.model.participant, $scope.model.team);
            }).then(function (response) {
                $modalInstance.close($scope.model.participant);
            });
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    };

    app.controller("ModalParticipantController", modalParticipantController);
}());