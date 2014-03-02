(function () {
    "use strict";

    var modalParticipantController = function ($scope, $modalInstance, $http, participant, team) {
        $scope.model = {};
        
        $scope.model.title = team.Name;
        $scope.model.participant = participant || {};
        $scope.model.team = team || {};

        $scope.save = function () {
            $http.post('/Team/Create', 
                $scope.model.team
            ).success(function (data) {
                $scope.model.team.Id = data.Id;

                $http.post('/Participant/Create',
                    $scope.model.participant
                ).success(function (data) {
                    $scope.model.participant.Id = data.Id;

                    $http.post('/Contingent/AssignParticipantToTeam', {
                        Id: $scope.model.participant.Id,
                        TeamId: $scope.model.team.Id
                    }).success(function (data) {
                        console.log(data);
                    });
                });
            });
            $modalInstance.close($scope.model.participant);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    };

    app.controller("ModalParticipantController", modalParticipantController);
}());