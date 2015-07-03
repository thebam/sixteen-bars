
var trackApp = angular.module("TrackApp", []);
trackApp.controller("TrackController", function ($scope, $http) {
    $scope.artistName;

    $scope.autoCompleteArtist = function () {
        if ($scope.artistName.length > 2) {
            $http({
                url: '/api/ArtistAPI/AutoCompleteName/'+ $scope.artistName,
                method: 'GET'
            }).success(function (data, status, headers, config) {
                $scope.artists = data.Data;
            });
        }
    };
});