(function () {
    "use strict";

    var scoreEntryController = function ($scope, $http, $q, $location, modalFactory, dataService) {
        var url = $location.absUrl();
        var lastSlash = url.lastIndexOf('/');
        var province = url.slice(lastSlash + 1);

        var pages = [
            'Division',
            'Game',
            'Match',
            'Score',
            'Result',
            'Submit'
        ];

        $scope.model = {};
        navigate('Division');

        $scope.Back = function () {
            var currentIndex = pages.indexOf($scope.page);
            var prevPage = pages[currentIndex - 1];
            navigate(prevPage);
        };

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
                    var teamSize = Math.max($scope.model.Away.Bowlers.length, $scope.model.Home.Bowlers.length) || 0;
                    $scope.model.Away.Score = $scope.model.Away.POA = $scope.model.Away.TotalPoints = 0;
                    $scope.model.Home.Score = $scope.model.Home.POA = $scope.model.Home.TotalPoints = 0;
                    
                    for (var i = 1; i <= teamSize; i++) {
                        updateScoreInfo(i);
                    }
                    $scope.model.Home.TotalPoints += $scope.model.Home.Point;
                    $scope.model.Away.TotalPoints += $scope.model.Away.Point;
                    break;
                }
                case 'Submit': {

                    break;
                }
            };

            navigate(page);
        };

        $scope.ValidForm = function () {
            var isValid = true;

            isValid = isValid && !!$scope.model.Away.Bowlers.length;
            isValid = isValid && !!$scope.model.Home.Bowlers.length;
            if (!isValid)
                return false;

            isValid = isValid && !$.grep($scope.model.Away.Bowlers, function (o) { return o.Score < 0 || o.Score > 450; }).length;
            isValid = isValid && !$.grep($scope.model.Home.Bowlers, function (o) { return o.Score < 0 || o.Score > 450; }).length;
            if (!isValid)
                return false;

            for (var i = 1; i <= 5; i++) {
                isValid = isValid && !!$.grep($scope.model.Away.Bowlers, function (o) { return o.Position == i; }).length;
                isValid = isValid && !!$.grep($scope.model.Home.Bowlers, function (o) { return o.Position == i; }).length;
            }

            return isValid;
        };

        function updateScoreInfo(position) {
            var homeBowler = ($.grep($scope.model.Home.Bowlers, function (o) { return o.Position == position; }) || [])[0];
            var awayBowler = ($.grep($scope.model.Away.Bowlers, function (o) { return o.Position == position; }) || [])[0];

            $scope.model.Home.Score += homeBowler.Score;
            $scope.model.Away.Score += awayBowler.Score;

            homeBowler.POA = homeBowler.Score - homeBowler.Average;
            awayBowler.POA = awayBowler.Score - awayBowler.Average;

            $scope.model.Home.POA += homeBowler.POA;
            $scope.model.Away.POA += awayBowler.POA;

            if (homeBowler.POA > awayBowler.POA) {
                homeBowler.Point = 1;
                awayBowler.Point = 0;
            }
            else if (homeBowler.POA < awayBowler.POA) {
                homeBowler.Point = 0;
                awayBowler.Point = 1;
            }
            else {
                homeBowler.Point = .5;
                awayBowler.Point = .5;
            }
            $scope.model.Home.TotalPoints += homeBowler.Point;
            $scope.model.Away.TotalPoints += awayBowler.Point;

            if ($scope.model.Home.POA > $scope.model.Away.POA) {
                $scope.model.Home.Point = 3;
                $scope.model.Away.Point = 0;
            }
            else if ($scope.model.Home.POA < $scope.model.Away.POA) {
                $scope.model.Home.Point = 0;
                $scope.model.Away.Point = 3;
            }
            else {
                $scope.model.Home.Point = 1.5;
                $scope.model.Away.Point = 1.5;
            }
        };

        function navigate(page) {
            $scope.page = page;
            $scope.viewUrl = '/App/Views/ScoreEntry/' + page + '.html';
        };
    };

    app.controller("ScoreEntryController", ["$scope", "$http", "$q", "$location", "modalFactory", "dataService", scoreEntryController]);
}());