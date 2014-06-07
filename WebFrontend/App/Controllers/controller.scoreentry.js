(function () {
    "use strict";

    var scoreEntryController = function ($scope, $http, $q, $location, modalFactory, dataService) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var province = url.slice(lastSlash + 1);

        $scope.model = {};
        $scope.viewUrl = '/App/Views/ScoreEntry/Division.html';

        $scope.Page = function (page, data) {
            switch (page) {
                case 'Game': {
                    dataService.LoadSchedule(data.Division).
                        success(function (divisionSchedule) {
                            $scope.model.Schedule = divisionSchedule;
                        });
                    $scope.model.Division = data.Division;
                    break;
                }
                case 'Match': {
                    $scope.model.Game = data.Game;
                    break;
                }
                case 'Score': {
                    $scope.model.AwayProvince = data.Away;
                    $scope.model.HomeProvince = data.Home;

                    dataService.LoadTeam(data.Away, $scope.model.Division).success(function (awayTeam) {
                        $scope.model.Away = awayTeam;
                    });
                    dataService.LoadTeam(data.Home, $scope.model.Division).success(function (homeTeam) {
                        $scope.model.Home = homeTeam;
                    });

                    break;
                }
                case 'Result': {
                    
                    break;
                }
            };

            $scope.ValidForm = function () {

            };

            $scope.viewUrl = '/App/Views/ScoreEntry/' + page + '.html';
        };
    };

    app.controller("ScoreEntryController", ["$scope", "$http", "$q", "$location", "modalFactory", "dataService", scoreEntryController]);
}());