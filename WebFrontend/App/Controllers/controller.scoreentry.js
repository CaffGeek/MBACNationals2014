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
                    $scope.model.Division = data.Division;
                    break;
                }
            }

            $scope.viewUrl = '/App/Views/ScoreEntry/' + page + '.html';
        }
    };

    app.controller("ScoreEntryController", ["$scope", "$http", "$q", "$location", "modalFactory", "dataService", scoreEntryController]);
}());