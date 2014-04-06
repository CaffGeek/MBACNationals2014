angular.module('numberFilters', []).filter('rounddown', function () {
    return function (input) {
        return Math.floor(input);
    };
});