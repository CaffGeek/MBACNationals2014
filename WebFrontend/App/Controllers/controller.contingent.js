(function () {
    "use strict";

    var contingentController = function ($scope, $http, $q, $location, modalFactory, dataService) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var province = url.slice(lastSlash+1);

        $scope.model = {
            Teams: [],
            Guests: []
        };

        if (province) {
            dataService.LoadContingent(province).
                success(function (contingent) {
                    $scope.model = contingent;

                    if (!$scope.model.Teams.length) {
                        editDivisions($scope.model.Teams);
                    }
                }); 
        }
        
        $scope.editDivisions = editDivisions;
        $scope.editTeam = editTeam;
        $scope.editParticipant = editParticipant;
        $scope.addGuest = addGuest;
        $scope.editGuest = editGuest;
        
        function editDivisions(teams) {
            var modalPromise = modalFactory.Divisions(teams);

            modalPromise.then(function (data) {
                //var currentTeams = $scope.model.Teams;
                //TODO: $scope.model.Teams = $.extend(true, [], currentTeams, data);
                
                //Remove division
                angular.forEach($scope.model.Teams, function (team, idx) {
                    if (data.filter(function (obj) { return obj.Name == team.Name; }).length === 0)
                    {
                        dataService.RemoveTeam(team, $scope.model); //Fire and forget
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

                    var team = $scope.model.Teams[i];
                    if (!(team && team.Bowlers && team.Bowlers.length && team.Bowlers[0].Id)) {
                        editTeam(team).then(function () {
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

            var editBowlerModal = function (i) {
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
                    editBowlerModal(i + 1);
                });
            };

            var modalPromise;
            if (team.RequiresCoach) {
                team.Coach = team.Coach || { IsCoach: true };
                modalPromise = editParticipant(team.Coach, team);
            }

            if (modalPromise)
                modalPromise.then(function () {
                    editBowlerModal(0);
                });
            else
                editBowlerModal(0);
            
            return dfd.promise;
        };

        function editParticipant(participant, team) {
            return dataService.LoadParticipant(participant.Id).then(function (data) {
                return modalFactory.Participant(data.data || participant, team).then(function (data) {
                    participant = data; //TODO: write back to scope somehow
                });
            });
        };

        function addGuest() {
            return modalFactory.Participant({ IsGuest: true }).
                then(function (data) {
                    dataService.AssignParticipantToContingent(data, $scope.model);
                }).
                then(function (data) {
                    $scope.model.Guests.push(data); //TODO: write back to scope somehow
                });
        };

        function editGuest(participant) {
            alert('Edit Guest')
        };
    };

    app.controller("ContingentController", ["$scope", "$http", "$q", "$location", "modalFactory", "dataService", contingentController]);
}());