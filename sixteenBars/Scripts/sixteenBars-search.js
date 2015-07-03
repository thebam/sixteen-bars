
            var searchApp = angular.module('SearchApp', []);

searchApp.controller('SearchController', function ($scope, $http) {
    $scope.searchTerm;
    $scope.searchType;
                
    $scope.search = function () {
        if ($scope.searchType == "" || $scope.searchType == undefined) {
            $scope.searchType = "quote";
        }
        $http({
            url: '/home/search',
            data: { "searchTerm": $scope.searchTerm, "searchType": $scope.searchType },
            method: 'POST'
        }).success(function (data, status, headers, config) {
            $scope.searchTitle = angular.uppercase($scope.searchType) + "S : '" + $scope.searchTerm + "'";
            $scope.quotes = data;
        });
    };

});
