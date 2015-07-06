///first docume
var trackApp = angular.module("TrackApp", []);
trackApp.controller("TrackController", function ($scope, $http) {
    $scope.autoCompleteArtist = function (targetField) {
        var inputArtistName = "";
        if (targetField === "artistName")
            inputArtistName = $scope.artistName;
        else {
            inputArtistName = $scope.artistName;
        }
        if (inputArtistName.length > 2) {
            $http({
                url: '/api/ArtistAPI/AutoCompleteName/' + inputArtistName,
                method: 'GET'
            }).success(function (data, status, headers, config) {
                $scope.artists = data.Data;
            }).error(function (error) {
                $scope.artists = "";
            });
        } else {
            $scope.artists = "";
        }
    };

    $scope.selectSuggestedArtist = function (artistName, fieldToPopulate) {
        if (fieldToPopulate === "artistName") {
            $scope.artistName = artistName;
        } else {
            $scope.artistName = artistName;
        }
        $scope.artists = "";
    }
});

//var trackApp = angular.module("TrackApp", []);
//trackApp.factory("artist", function ($http) {
//    return {
//        autoComplete: function (artistName) {
//            return $http.get('/api/ArtistAPI/AutoCompleteName/' + artistName);
//        }
//    }
//});

//trackApp.controller("TrackController", function ($scope, artist, $http) {
//    $scope.artistName;
//    $scope.autoCompleteArtist = function () {
//        if ($scope.artistName.length > 2) {
//            artist.autoComplete().success(function (data) {
//                $scope.data = data.Data;
//            });
//        }
//    };

    
//});

//var trackApp = angular.module('TrackApp', []);

//trackApp.factory('artist', function ($http) {
//    return {
//        autoComplete: function (artistName) {
//            if (artistName.length > 2) {
//                return $http.get('/api/ArtistAPI/AutoCompleteName/' + artistName);
//            }
//        }
//    }
//});



//trackApp.controller('TrackController', function ($scope, artist, $http) {
//    $scope.autoCompleteArtist = function () {
//        artist.autoComplete($scope.artistName).success(function (data) {
//            $scope.artists = data;
//        }).error(function (error) {
//            $scope.artists = "";
//            $scope.status = 'Something is wrong: ' + error.message;
//        });
//    };
//});
