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
                var currentTeams = $scope.model.Teams;
                
                $scope.model.Teams = $.extend(true, [], currentTeams, data);

                //TODO: Save all the selected teams and assign them to the contingent
                angular.forEach($scope.model.Teams, function (team) {
                    if (!team.Selected || team.ContingentId)
                        return;
                                        
                    dataService.SaveTeam(team, $scope.model).then(function (data) {
                        team.Id = data.data.TeamId;
                    }).then(function (data) {
                        angular.forEach($scope.model.Teams, function (team) {
                            team.Bowlers = team.Bowlers || [];
                            while (team.Bowlers.length < team.SizeLimit) {
                                team.Bowlers.push({ Gender: team.Gender });
                            }
                        });
                        editTeamModal(0);
                    });
                });
                

                var editTeamModal = function (i) {
                    if (i >= $scope.model.Teams.length)
                        return;

                    var team = $scope.model.Teams[i];
                    
                    if (team.Selected) {
                        var modalPromise = editTeam(team);
                        modalPromise.then(function () {
                            editTeamModal(i + 1);
                        });
                    } else {
                        editTeamModal(i + 1);
                    }
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