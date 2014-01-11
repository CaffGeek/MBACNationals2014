(function () {
    "use strict";

    var contingentController = function ($scope, $http, $q, modalFactory) {
        $scope.model = {};

        $scope.model.Province = "MB";
        $scope.model.Teams = $scope.model.Teams || [];

        $scope.editDivisions = editDivisions;
        $scope.editTeam = editTeam;
        $scope.editParticipant = editParticipant;

        if (!$scope.model.Teams.length) {
            editDivisions();
        }

        function editDivisions() {
            var modalPromise = modalFactory.Divisions($scope.model.Teams);

            modalPromise.then(function (data) {
                var currentTeams = $scope.model.Teams;
                var updatedTeams = data.filter(function (obj) { return obj.Selected; });
                
                //TODO: Properly merge teams
                var mergedTeams = updatedTeams;
                $scope.model.Teams = mergedTeams;

                angular.forEach($scope.model.Teams, function (team) {
                    team.Bowlers = team.Bowlers || [];
                    while (team.Bowlers.length < team.SizeLimit) {
                        team.Bowlers.push({ Gender: team.Gender });
                    }
                });

                var editTeamModal = function (i) {
                    if (i >= $scope.model.Teams.length)
                        return;

                    var team = $scope.model.Teams[i];
                    
                    var modalPromise = editTeam(team);
                    modalPromise.then(function () {
                        editTeamModal(i + 1);
                    });
                };
                editTeamModal(0);
            });
        };

        function editTeam(team) {
            var dfd = $q.defer();

            team.Bowlers = team.Bowlers || [];

            var editParticipantModal = function (i) {
                if (i >= team.SizeLimit)
                {
                    dfd.resolve();
                    return;
                }

                var participant = team.Bowlers[i];
                if (!participant) {
                    participant = { Gender: team.Gender }; //TODO: Use a Factory
                    team.Bowlers.push(participant);
                }

                var modalPromise = editParticipant(participant, team);
                modalPromise.then(function (data) {
                    editParticipantModal(i + 1);
                });
            };
            editParticipantModal(0);

            return dfd.promise;
        };

        function editParticipant(participant, team) {
            return modalFactory.Participant(participant, team);
        };
    };

    app.controller("ContingentController", contingentController);
}());