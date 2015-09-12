
            var searchApp = angular.module('SearchApp', []);

searchApp.controller('SearchController', function ($scope, $http) {
    $scope.searchTerm;
    $scope.searchType;
                
    $scope.search = function () {
        if ($scope.searchType == "" || $scope.searchType == undefined) {
            $scope.searchType = "quote";
        }
        
        //$http({
        //    url: siteURL + '/api/SearchAPI/Search?searchTerm='+$scope.searchTerm+'&searchType=' + $scope.searchType,
        //    method: 'GET'
        //}).success(function (data, status, headers, config) {
        //    $scope.searchTitle = angular.uppercase($scope.searchType) + "S : '" + $scope.searchTerm + "'";
        //    $scope.quotes = data.Data;
        //});

        var baseUrl = siteURL + "/api/SearchAPI/Search?searchTerm="+$scope.searchTerm+"&searchType=" + $scope.searchType;
        var promise = $http.get(baseUrl);
        promise.then(function (payload) {
            $scope.quotes = payload.data.Data;
        },
            function (errorPayload) {
                $scope.quotes = "";
            });
    };

});
