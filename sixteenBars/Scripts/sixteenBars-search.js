
            var searchApp = angular.module('SearchApp', []);

searchApp.controller('SearchController', function ($scope, $http) {
    $scope.searchTerm;
    $scope.searchType;
    $scope.searchtermTitle = false;
    $scope.searchNoResults = false;
                
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

        var baseUrl = siteURL + "/api/SearchAPI/Search?searchTerm=" + $scope.searchTerm + "&searchType=" + $scope.searchType + "&wordLink=false&filter=" + getCookie();
        var promise = $http.get(baseUrl);
        promise.then(function (payload) {
            $scope.searchtermTitle = true;
            if (payload.data.Data.length > 0) {
                $scope.results = payload.data.Data;
                $scope.searchNoResults = false;
            } else {

                if (getCookie()==false) {
                    alert('Try setting the language filter to "Explicit".');
                }
                $scope.searchNoResults = true;
            }
        },
            function (errorPayload) {
                $scope.results = "";
                $scope.searchtermTitle = true;
                $scope.searchNoResults = true;
            });
    };

});
