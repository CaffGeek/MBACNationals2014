(function () {
    "use strict";

    var modalParticipantController = function ($scope, $modalInstance, $http, participant, team) {
        $scope.model = {};
        
        $scope.model.title = '{0} {1}'.format(participant.Id ? 'Edit' : 'New', team.Name);
        $scope.model.participant = participant || {};
        $scope.model.team = team || {};

        $scope.save = function () {
            $modalInstance.close($scope.model.participant);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    };

    app.controller("ModalParticipantController", modalParticipantController);
}());