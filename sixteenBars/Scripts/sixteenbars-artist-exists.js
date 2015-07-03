
var artistApp = angular.module("ArtistApp", []);
var artistController = artistApp.controller("ArtistController", function ($scope, $http) {
    $scope.ArtistName;
    $scope.artistExists = false;
    $scope.artistExistsSubmit = true;
    $scope.autoCompleteArtistName = function () {
        if ($scope.ArtistName.length > 2) {
            $http({
                url: '/api/ArtistAPI/ArtistExists',
                data: "'" + $scope.ArtistName + "'",
                method: 'POST'
            })
            .success(function (data, status, headers, config) {
                if (data === true) {
                    $scope.artistExists = true;
                    $scope.artistExistsSubmit = false;
                } else {
                    $scope.artistExists = false;
                    $scope.artistExistsSubmit = true;
                }
            });
        }
    };
});
