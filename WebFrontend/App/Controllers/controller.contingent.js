(function () {
    "use strict";

    var contingentController = function ($scope, $http, $q, modalFactory, dataService) {
        var province = "MB"; //TODO: load from url or something

        $scope.model = {
            Teams: []
        };
        //$scope.model.Province = province;
        //$scope.model.Teams = $scope.model.Teams || []; //TODO: Use a Factory

        if (province) {
            dataService.LoadContingent(province).
                success(function (contingent) {
                    $scope.model = contingent;
                    editDivisions($scope.model.Teams);
                }); 
        } else {
            if (!$scope.model.Teams.length) {
                editDivisions($scope.model.Teams);
            }
        }
        
        $scope.editDivisions = editDivisions;
        $scope.editTeam = editTeam;
        $scope.editParticipant = editParticipant;
        
        function editDivisions(teams) {
            var modalPromise = modalFactory.Divisions(teams);

            modalPromise.then(function (data) {
                //var currentTeams = $scope.model.Teams;                
                //TODO: $scope.model.Teams = $.extend(true, [], currentTeams, data);

                //Remove division
                angular.forEach($scope.model.Teams, function (team, idx) {
                    if (data.filter(function (obj) { return obj.Name == team.Name; }).length === 0)
                    {
                        alert('remove ' + team.Name); 
                        $scope.model.Teams.splice(idx);
                    }
                });

                angular.forEach(data, function (team, idx) {
                    if ($scope.model.Teams.filter(function (obj) { return obj.Name == team.Name; }).length === 0) {
                        $scope.model.Teams.push(team);
                    }
                });
                
                var saveTeamPromises = [];
                angular.forEach($scope.model.Teams, function (team) {
                    var saveTeamPromise = dataService.SaveTeam(team, $scope.model).then(function (data) {
                        team.Id = data.data.TeamId;

                        team.Bowlers = team.Bowlers || [];
                        while (team.Bowlers.length < team.SizeLimit) {
                            team.Bowlers.push({ Gender: team.Gender });
                        }
                    });

                    saveTeamPromises.push(saveTeamPromise);
                });

                $q.all(saveTeamPromises).then(function (data) {
                    editTeamModal(0);  //TODO: Only open new teams for editing
                });

                var editTeamModal = function (i) {
                    if (i >= $scope.model.Teams.length)
                        return;

                    editTeam($scope.model.Teams[i]).then(function () {
                        editTeamModal(i + 1);
                    });
                };
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