
app.controller("ArtistController", function ($scope, $http, autoCompleteFactory) {
    $scope.autoCompleteArtist = function (targetField) {
        var inputArtistName = "";
        if (targetField === "artistName")
            inputArtistName = $scope.artistName;
        else {
            inputArtistName = $scope.artistName;
        }
        if (inputArtistName.length > 2) {
            var promise = autoCompleteFactory.artistName(inputArtistName);
            promise.then(function (payload) {
                $scope.artists = payload.data.Data;
            },
            function (errorPayload) {
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


//http://www.benlesh.com/2013/06/angular-js-unit-testing-services.html
//var trackApp = angular.module("TrackApp", []);
//trackApp.factory("httpBasedService", function ($http) {
//    return {
//        autoCompleteArtist: function (artistName) {
            
//                        return $http.get('/api/ArtistAPI/AutoCompleteName/' + artistName)
//                        .then(function (result) {
//                            return result.data;
//                        });
                    
//        }
//    }
//});

//trackApp.controller("TrackController", function ($scope, httpBasedService) {
//    $scope.autoCompleteArtist = function (targetField) {
//        var inputArtistName = "";
//        if (targetField === "artistName")
//            inputArtistName = $scope.artistName;
//        else {
//            inputArtistName = $scope.artistName;
//        }
//        if (inputArtistName.length > 2) {
//            var temp = httpBasedService.autoCompleteArtist(inputArtistName);
//            alert(temp);
//            $scope.artists = temp;
            
//        } else {
//            $scope.artists = "";
//        }
//    };
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
