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
                    $scope.model.Away = data.Away;
                    $scope.model.Home = data.Home;
                    break;
                }
            }

            $scope.viewUrl = '/App/Views/ScoreEntry/' + page + '.html';
        }
    };

    app.controller("ScoreEntryController", ["$scope", "$http", "$q", "$location", "modalFactory", "dataService", scoreEntryController]);
}());